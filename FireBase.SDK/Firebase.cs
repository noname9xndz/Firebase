using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FireBase.SDK.Models;
using Microsoft.Extensions.Options;
using Slack.SDK;

namespace FireBase.SDK
{
    public class Firebase : IFirebase
    {
        private readonly ISlackNotify _slackNotify;

        private FirebaseConfig _settings;
        public Firebase(IOptions<FirebaseConfig> settings, ISlackNotify slackNotify)
        {
            _settings = settings.Value;
            _slackNotify = slackNotify;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="token"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<FirebaseResource> GetFirebase(string token, string code)
        {
            FirebaseResource _firebase = await GetVerifyPhoneNumber(token, code);
            if (_firebase != null && _firebase.Error != null && _firebase.PhoneNumber < 10000)
            {
                //Neu gap loi kiem tra token đã được verify ở client chưa
                JwtSecurityToken jwt = await GetJwtSecurityToken(token);
                if (jwt != null)
                {
                    DateTime timeNow = DateTime.UtcNow;
                    if (jwt.ValidFrom < timeNow && timeNow < jwt.ValidTo && jwt.Audiences.Contains(_settings.ProjectId))
                    {
                        //token ok
                        //goi hàm lấy thông tin từ api firebase và mapping thành Firebase Resource
                        FirebaseResource _firebaseToken = await GetUserInfo(token);
                        if (_firebaseToken != null) return _firebaseToken;
                    }
                }
            }
            return _firebase;
        }

        /// <summary>
        /// Gửi thông báo đẩy trên app Học Hay
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PushNotification(string title, string body, string image)
        {
            if (!string.IsNullOrWhiteSpace(_settings.PushNotificationUrl))
            {
                HttpResponseMessage result = new HttpResponseMessage();
                var notification = new { title = title, body = body, image = image };
                var data = new { to = "/topics/all", notification = notification };
                string dataPost = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                StringContent content = new StringContent(dataPost, Encoding.UTF8, "application/json");
                using (HttpClient webClient = new HttpClient())
                {
                    webClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=" + _settings.ServerKey);
                    result = await webClient.PostAsync(_settings.PushNotificationUrl, content);
                }
                return result;
            }

            return null;
        }

        private async Task<FirebaseResource> GetFirebaseSessionInfoOTP(string sessionInfo, string otp)
        {
            var dataPost = new { sessionInfo = sessionInfo, code = otp };
            string urlApi = _settings.SessionInfoUrl + _settings.ServerKey;
            string jsonObject = Newtonsoft.Json.JsonConvert.SerializeObject(dataPost);
            StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            using (HttpClient webClient = new HttpClient())
            {
                HttpResponseMessage result = await webClient.PostAsync(urlApi, content);
                jsonObject = await result.Content.ReadAsStringAsync();
            }
            FirebaseResource resource = Newtonsoft.Json.JsonConvert.DeserializeObject<FirebaseResource>(jsonObject);
            return resource;
        }

        private async Task<FirebaseResource> VerifyPhoneNumber(string sessionInfo, string otp)
        {
            try
            {
                var dataPost = new { sessionInfo = sessionInfo, code = otp };
                string urlApi = _settings.VerifyPhoneNumberUrl + _settings.ServerKey;
                var jsonObject = Newtonsoft.Json.JsonConvert.SerializeObject(dataPost);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                using (HttpClient webClient = new HttpClient())
                {
                    var result = await webClient.PostAsync(urlApi, content);
                    jsonObject = await result.Content.ReadAsStringAsync();
                }
                var resource = Newtonsoft.Json.JsonConvert.DeserializeObject<FirebaseResource>(jsonObject);
                resource.RefreshToken = urlApi;
                return resource;
            }
            catch (Exception ex)
            {
                if(_settings.UseSlackNotify)
                    await _slackNotify.SendErrorException(ex);
            }
            return new FirebaseResource();
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<JwtSecurityToken> GetJwtSecurityToken(string token)
        {
            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                Microsoft.IdentityModel.Tokens.SecurityToken jsonToken = handler.ReadToken(token);
                JwtSecurityToken tokenS = jsonToken as JwtSecurityToken;
                return tokenS;
            }
            catch (Exception ex)
            {
                if (_settings.UseSlackNotify)
                    await _slackNotify.SendErrorException(ex);
            }
            return null;
        }

        /// <summary>
        /// API: https://firebase.google.com/docs/reference/rest/auth#section-get-account-info
        /// </summary>
        /// <param name="tokenFirebase"></param>
        /// <returns></returns>
        private async Task<FirebaseResource> GetUserInfo(string tokenFirebase)
        {
            var dataPost = new { idToken = tokenFirebase };
            string urlApi = _settings.GetAccountInfoUrl + _settings.ServerKey;
            string jsonObject = Newtonsoft.Json.JsonConvert.SerializeObject(dataPost);
            StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            using (HttpClient webClient = new HttpClient())
            {
                HttpResponseMessage result = await webClient.PostAsync(urlApi, content);
                jsonObject = await result.Content.ReadAsStringAsync();
            }
            UserDataFirebase resource = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDataFirebase>(jsonObject);
            if (resource != null)
            {
                UserFirebase user = resource.Users.FirstOrDefault();
                if (user != null)
                {
                    FirebaseResource firebaseResource = new FirebaseResource
                    {
                        IdToken = tokenFirebase,
                        LocalId = user.LocalId,
                        PhoneNumber = user.PhoneNumber,
                    };
                    return firebaseResource;
                }
            }
            return null;
        }

        private async Task<FirebaseResource> GetVerifyPhoneNumber(string sessionInfo, string otp)
        {
            try
            {
                FirebaseResource data = new FirebaseResource();
                if (_settings.UseLamdbaFunction && !string.IsNullOrWhiteSpace(_settings.UrlVerifyPhoneNumberLambda))
                {
                    data = await GetFirebaseSessionInfoOTPbyLamda(sessionInfo, otp);
                }
                else
                {
                    data = await VerifyPhoneNumber(sessionInfo, otp);
                }
                if (data != null) return data;
            }
            catch (Exception ex)
            {
                if (_settings.UseSlackNotify)
                    await _slackNotify.SendErrorException(ex);
            }
            return await GetFirebaseSessionInfoOTP(sessionInfo, otp);
        }

        private async Task<FirebaseResource> GetFirebaseSessionInfoOTPbyLamda(string sessionInfo, string otp)
        {
            if (!string.IsNullOrWhiteSpace(_settings.UrlVerifyPhoneNumberLambda))
            {
                try
                {
                    var dataPost = new { SessionInfo = sessionInfo, OTP = otp, ServerKey = _settings.ServerKey };
                    string jsonObject = Newtonsoft.Json.JsonConvert.SerializeObject(dataPost);
                    StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                    using (HttpClient webClient = new HttpClient())
                    {
                        HttpResponseMessage result = await webClient.PostAsync(_settings.UrlVerifyPhoneNumberLambda, content);
                        jsonObject = await result.Content.ReadAsStringAsync();
                    }
                    FirebaseResource resource = Newtonsoft.Json.JsonConvert.DeserializeObject<FirebaseResource>(jsonObject);
                    return resource;
                }
                catch (Exception ex)
                {
                    if (_settings.UseSlackNotify)
                        await _slackNotify.SendErrorException(ex);
                }
            }
            return null;
        }
    }
}
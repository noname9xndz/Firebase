using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Firebase.AWS.Verify.Models;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Firebase.AWS.Verify
{
    public class FirebaseFunction
    {
        public async Task<FirebaseResource> VerifyPhoneNumber(FirebaseVerifyNumberRequest request)
        {
            var dataPost = new { sessionInfo = request.SessionInfo, code = request.OTP };
            string urlApi = $"https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPhoneNumber?key={request.ServerKey}";
            var jsonObject = Newtonsoft.Json.JsonConvert.SerializeObject(dataPost);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            using (HttpClient webClient = new HttpClient())
            {
                var result = await webClient.PostAsync(urlApi, content);
                jsonObject = await result.Content.ReadAsStringAsync();
            }
            var resource = Newtonsoft.Json.JsonConvert.DeserializeObject<FirebaseResource>(jsonObject);
            resource.refreshToken = urlApi;
            return resource;
        }
    }
}
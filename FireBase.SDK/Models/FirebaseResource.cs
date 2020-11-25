using Newtonsoft.Json;

namespace Firebase.SDK.Models
{
    public class FirebaseResource
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("error")]
        public FirebaseOtpError Error { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("expiresIn")]
        public string ExpiresIn { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("idToken")]
        public string IdToken { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("isNewUser")]
        public bool IsNewUser { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("localId")]
        public string LocalId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("phoneNumber")]
        public long PhoneNumber { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
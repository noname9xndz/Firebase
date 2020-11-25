using Newtonsoft.Json;

namespace Firebase.SDK.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ProviderUserInfo
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("providerId")]
        public string ProviderId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("rawId")]
        public string RawId { get; set; }
    }
}
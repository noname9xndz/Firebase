using Newtonsoft.Json;

namespace FireBase.SDK.Models
{
    /// <summary>
    ///
    /// </summary>
    public class Provideruserinfo
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
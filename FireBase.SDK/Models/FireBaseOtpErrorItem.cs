using Newtonsoft.Json;

namespace Firebase.SDK.Models
{
    /// <summary>
    /// item lỗi
    /// </summary>
    public class FirebaseOtpErrorItem
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// mesage error
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
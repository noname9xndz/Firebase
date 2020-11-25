using System;
using Newtonsoft.Json;

namespace Firebase.SDK.Models
{
    public class UserFirebase
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }

        /// <summary>
        ///
        /// </summary>
         [JsonProperty("lastLoginAt")]
        public long LastLoginAt { get; set; }

        /// <summary>
        ///
        /// </summary>

        [JsonProperty("lastRefreshAt")]
        public DateTime LastRefreshAt { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("localId")]
        public string LocalId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("providerUserInfo")]
        public ProviderUserInfo[] ProviderUserInfo { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("phoneNumber")]
        public long PhoneNumber { get; set; }
    }
}
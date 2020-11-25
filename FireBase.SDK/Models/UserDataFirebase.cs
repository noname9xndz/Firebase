using System.Collections.Generic;
using Newtonsoft.Json;

namespace Firebase.SDK.Models
{
    public class UserDataFirebase
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("kind")]
        public string Kind { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("users")]
        public List<UserFirebase> Users { get; set; }
    }
}
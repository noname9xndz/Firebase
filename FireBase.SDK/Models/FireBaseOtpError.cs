using System.Collections.Generic;
using Newtonsoft.Json;

namespace FireBase.SDK.Models
{
    /// <summary>
    /// lỗi khi xác nhận otp
    /// </summary>
    public class FireBaseOtpError
    {
        /// <summary>
        /// 400 - message SESSION_EXPIRED, INVALID_CODE
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// danh sách lỗi
        /// </summary>
        [JsonProperty("errors")]
        public List<FireBaseOtpErrorItem> Errors { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
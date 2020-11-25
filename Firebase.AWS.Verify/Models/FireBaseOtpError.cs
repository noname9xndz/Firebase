using System.Collections.Generic;

namespace Firebase.AWS.Verify.Models
{
    /// <summary>
    /// lỗi khi xác nhận otp
    /// </summary>
    public class FireBaseOtpError
    {
        public FireBaseOtpError()
        {
            errors = new List<FireBaseOtpErrorItem>();
        }

        /// <summary>
        /// 400 - message SESSION_EXPIRED, INVALID_CODE
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// danh sách lỗi
        /// </summary>
        public List<FireBaseOtpErrorItem> errors { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string message { get; set; }
    }
}
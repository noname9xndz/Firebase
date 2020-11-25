using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FireBase.SDK.Models
{
    /// <summary>
    /// Form đăng ký tài khoản
    /// </summary>
    public class FormPostRegisterPhoneFirebase
    {
        /// <summary>
        /// Tên hiển thị người dùng
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { set; get; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetDisplayName()
        {
            if (!string.IsNullOrWhiteSpace(DisplayName)) return DisplayName;
            return $"{PhoneCountry}{PhoneNumber}";
        }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Required]
        [JsonProperty("phoneNumber")]
        public long PhoneNumber { get; set; }

        /// <summary>
        /// số thành phố
        /// </summary>
        [ServiceStack.DataAnnotations.Required]
        [JsonProperty("phoneCountry")]
        public int PhoneCountry { get; set; }

        /// <summary>
        /// Gets the phone.
        /// </summary>
        /// <returns></returns>
        public string GetPhone()
        {
            return $"{PhoneCountry}{PhoneNumber}";
        }

        /// <summary>
        /// Mật Khẩu
        /// </summary>
        [Required]
        [MinLength(6)]
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// user code
        /// </summary>
        [ServiceStack.DataAnnotations.Required]
        [JsonProperty("codeOTP")]
        public string CodeOtp { get; set; }

        /// <summary>
        /// nhận 2 giá trị là sessionInfo và idToken
        /// <para>1. mặc định ở client trả về sessionInfo chưa verify => token = sessionInfo</para>
        /// <para>2. vẫn trả về sessionInfo như đã verify => token = idToken </para>
        /// </summary>
        [Required]
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("projectId")]
        public string ProjectId { get; set; }
    }
}
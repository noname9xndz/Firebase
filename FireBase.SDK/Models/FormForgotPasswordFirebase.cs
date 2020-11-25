using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FireBase.SDK.Models
{
    /// <summary>
    ///
    /// </summary>
    public class FormForgotPasswordFirebase
    {
        /// <summary>
        /// Tên hiển thị người dùng
        /// </summary>
        [Required]
        [JsonProperty("userName")]
        public string UserName { set; get; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Required]
        [JsonProperty("phoneNumber")]
        public long PhoneNumber { get; set; }

        /// <summary>
        /// số thành phố
        /// </summary>
        [Required]
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
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// user code
        /// </summary>
        [Required]
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
namespace Firebase.SDK.Models
{
    public class FirebaseConfig
    {
        /// <summary>
        ///
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        public string ProjectId { set; get; }

        /// <summary>
        ///
        /// </summary>
        public string ServerKey { get; set; }
        public string PushNotificationUrl { get; set; }
        public string GetAccountInfoUrl { get; set; }
        public string VerifyPhoneNumberUrl { get; set; }
        public string SessionInfoUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool UseLamdbaFunction { get; set; }
        public string UrlVerifyPhoneNumberLambda { get; set; }

        public bool UseSlackNotify { get; set; }
    }
}
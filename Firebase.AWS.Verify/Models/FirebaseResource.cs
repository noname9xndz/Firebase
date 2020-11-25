namespace Firebase.AWS.Verify.Models
{
    /// <summary>
    ///
    /// </summary>
    public class FirebaseResource
    {
        /// <summary>
        ///
        /// </summary>
        public FireBaseOtpError error { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string expiresIn { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string idToken { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool isNewUser { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string localId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public long phoneNumber { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string refreshToken { get; set; }
    }
}
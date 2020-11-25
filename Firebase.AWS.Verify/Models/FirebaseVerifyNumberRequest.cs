namespace Firebase.AWS.Verify.Models
{
    public class FirebaseVerifyNumberRequest
    {
        public string SessionInfo { set; get; }
        public string OTP { set; get; }
        public string ServerKey { set; get; }
    }
}
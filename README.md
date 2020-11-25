# Firebase

Step 1 : Add Setting file for Firebase

{
  "FirebaseConfig": {
    "AppId": "",
    "ApiKey": "",
    "Server_Key": "",
    "ProjectId": "",
    "UseLamdbaFunction": false,
    "UrlVerifyPhoneNumberLambda": "",
    "UseSlackNotify": false,
    "PushNotificationUrl": "https://fcm.googleapis.com/fcm/send",
    "GetAccountInfoUrl": "https://identitytoolkit.googleapis.com/v1/accounts:lookup?key=",
    "VerifyPhoneNumberUrl": "https://identitytoolkit.googleapis.com/v1/accounts:lookup?key=",
    "SessionInfoUrl": "https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPhoneNumber?key="
  }
 
}

If you use Slack Noify Add
{
  "SlackConfig": {
    "ChannelSentryUrl": "",
    "ChannelUserRegUrl": "",
    "ChannelEditorUrl": "",
    "ChannelUserPaymentUrl": ""
  }
}

If you use lamdba function , you need add UrlVerifyPhoneNumberLambda and active UseLamdbaFunction 
and deploy function Firebase.AWS.Verify to AWS Lamdba Function

Step2: register

public void ConfigureServices(IServiceCollection services)
{
       
       services.Configure<SlackOptions>(options =>configuration.GetSection("SlackConfig").Bind(options));
       services.AddScoped<ISlackClient, SlackClient>();
       services.AddScoped<ISlackNotify, SlackNotify>();

       services.Configure<FirebaseConfig>(options => configuration.GetSection("FirebaseConfig").Bind(options));
       services.AddScoped<IFirebase, Firebase>();
}

Or 

public void ConfigureServices(IServiceCollection services)
{
       
       FirebaseIoc.Register(services,Configuration)
}

+ Use :

public class TestController : Controller
{
     private readonly IFirebase _firebase;
     public TestController(IFirebase firebase)
     {
         _firebase = firebase
     }
}




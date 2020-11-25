using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Firebase.SDK.Models;

namespace Firebase.SDK
{
   public interface IFirebase
   {
       Task<FirebaseResource> GetFirebase(string token, string code);

       Task<HttpResponseMessage> PushNotification(string title, string body, string image);

   }
}

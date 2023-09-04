using Immowert4You.Presentation.Account.Models;
using Presentation.Droid;
using Plugin.FirebasePushNotification;
using Firebase;
using Firebase.Messaging;
using Firebase.Installations;
using Android.Gms.Extensions;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceTokenManager))]
namespace Presentation.Droid
{
    public class DeviceTokenManager : IDeviceTokenManager
    {
        [System.Obsolete]
        public string GetDeviceToken()
        {
            var token = FirebaseMessaging.Instance.GetToken();
            while(!token.IsComplete){}

            return token.Result.ToString();
        }
    }
}
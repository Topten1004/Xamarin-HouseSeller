
using Firebase.CloudMessaging;
using Immowert4You.Presentation.Account.Models;
using Presentation.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceTokenManager))]
namespace Presentation.iOS
{
    public class DeviceTokenManager : IDeviceTokenManager
    {
        public string GetDeviceToken()
        {
            return Messaging.SharedInstance.FcmToken;
        }
    }
}
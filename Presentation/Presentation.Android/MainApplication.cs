using Android.App;
using Android.OS;
using Android.Runtime;
using System;
using Plugin.FirebasePushNotification;

namespace Presentation.Droid
{
    [Application]
    public class MainApplication : Application {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer) {
            
        }

        public override void OnCreate()
        {
            base.OnCreate();
            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "DefaultChannel";

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }


            //If debug you should reset the token each time.
            FirebasePushNotificationManager.Initialize(this, false);

            //Handle notification when app is closed here
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {


            };
        }
    }
}
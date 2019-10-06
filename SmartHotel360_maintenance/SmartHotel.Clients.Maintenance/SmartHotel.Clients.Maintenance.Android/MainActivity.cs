using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Essentials;
using Firebase.Iid;
using Firebase.Messaging;
using Android.Gms.Common;
using Firebase;
using System.Threading.Tasks;
using Xamarin.Forms;
using SmartHotel.Clients.Maintenance.ViewModels;

namespace SmartHotel.Clients.Maintenance.Droid
{
    [Activity(Label = "SmartHotel.Clients.Maintenance", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public MainActivity()
        {
            MessagingCenter.Subscribe<InputPageViewModel, string>(this, "Topic", (sender, arg) =>
            {
                FirebaseMessaging.Instance.SubscribeToTopic(arg);
            });
            MessagingCenter.Subscribe<MainPageViewModel, string>(this, "Topic", (sender, arg) =>
            {
                FirebaseMessaging.Instance.SubscribeToTopic(arg);
            });
        }

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FirebaseApp.InitializeApp(this);
            try
            {
                await SetTopic();

                await SecureStorage.SetAsync("Available", IsPlayServicesAvailable());

                string notiToken = FirebaseInstanceId.Instance.Token;

                if (!String.IsNullOrEmpty(notiToken))
                {
                    await SecureStorage.SetAsync("notiToken", notiToken);
                    App.StatusToken = true;
                }

                
            }
            catch (Exception e)
            {

                throw e;
            }
            LoadApplication(new App());
        }

        private string IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    return GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }
                else
                {
                    return "This device is not supported";
                }
            }
            else
            {
                return "Google Play Service is available";
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public async Task SetTopic()
        {
            try
            {
                var topic = await SecureStorage.GetAsync("Topic");

                if (String.IsNullOrEmpty(topic))
                {
                    topic = "0";
                }

                FirebaseMessaging.Instance.SubscribeToTopic(topic);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
﻿using System;

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

namespace SmartHotel.Clients.Maintenance.Droid
{
    [Activity(Label = "SmartHotel.Clients.Maintenance", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
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
                string notiToken = FirebaseInstanceId.Instance.Token;

                await SecureStorage.SetAsync("notiToken", notiToken);

                FirebaseMessaging.Instance.SubscribeToTopic("Group1_Topic");

                await SecureStorage.SetAsync("Available", IsPlayServicesAvailable());
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
    }
}
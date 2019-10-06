using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using Java.Lang;
using SmartHotel.Clients.Maintenance.Views;
using Xamarin.Forms;

namespace SmartHotel.Clients.Maintenance.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyMessagingService : FirebaseMessagingService, IMyMessagingService
    {
        private readonly string NOTIFICATION_CHANNEL_ID = "com.companyname.smarthotel.clients.maintenance";

        public override void OnMessageReceived(RemoteMessage message)
        {
            if (!message.Data.GetEnumerator().MoveNext())
            {
                SentNotification(message.GetNotification().Title, message.GetNotification().Body);
            }
            else
            {
                SentNotification(message.Data);
            }
            MessagingCenter.Send<IMyMessagingService>(this, "ReData");
        }

        private void SentNotification(IDictionary<string, string> data)
        {
            string title;
            string body;

            data.TryGetValue("title", out title);
            data.TryGetValue("body", out body);

            SentNotification(title, body);
        }

        private void SentNotification(string title, string body)
        {
            NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, "Notification Channel", Android.App.NotificationImportance.Default);

                notificationChannel.Description = "Enixer Group01";
                notificationChannel.EnableLights(true);
                notificationChannel.SetVibrationPattern(new long[] { 0, 1000, 500, 1000 });

                notificationManager.CreateNotificationChannel(notificationChannel);
            }

            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID);

            notificationBuilder.SetAutoCancel(true)
                .SetDefaults(-1)
                .SetWhen(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
                .SetContentTitle(title)
                .SetContentText(body)
                .SetSmallIcon(Resource.Drawable.noti)
                .SetContentInfo("info");

            notificationManager.Notify(new Random().Next(), notificationBuilder.Build());
        }
    }
}
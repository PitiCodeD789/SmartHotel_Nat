using SmartHotel.Clients.Maintenance.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHotel.Clients.Maintenance
{
    public partial class App : Application
    {
        public static bool StatusToken { get; set; } = false;
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new MainPage());
        }

        protected async override void OnStart()
        {
            if (StatusToken)
            {
                var topic = await SecureStorage.GetAsync("Topic");

                if (String.IsNullOrEmpty(topic))
                {
                    MainPage = new NavigationPage(new InputPage());
                }
                else
                {
                    MainPage = new NavigationPage(new MainPage());
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "บัญชีโรงแรมมีปัญหา", "OK");
            }
            
            //var available = await SecureStorage.GetAsync("Available");
            //await App.Current.MainPage.DisplayAlert("Message", available, "OK");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

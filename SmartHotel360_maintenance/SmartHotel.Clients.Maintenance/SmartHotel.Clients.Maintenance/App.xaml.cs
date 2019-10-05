using SmartHotel.Clients.Maintenance.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHotel.Clients.Maintenance
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected async override void OnStart()
        {
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

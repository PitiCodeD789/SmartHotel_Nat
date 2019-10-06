using SmartHotel.Clients.Maintenance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHotel.Clients.Maintenance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            if (!App.StatusToken)
            {
                DisplayAlert("Error", "ไม่สามารถเปิด Notification ได้\nกรุณาปิดและเปิด Application นี้ใหม่อีกครั้ง", "OK");
            }

            //var available = await SecureStorage.GetAsync("Available");
            //await App.Current.MainPage.DisplayAlert("Message", available, "OK");
        }
    }
}
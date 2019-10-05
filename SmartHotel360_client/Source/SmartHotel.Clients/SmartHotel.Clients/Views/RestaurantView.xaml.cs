using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHotel.Clients.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RestaurantView : ContentPage
    {
        public RestaurantView()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            StackLayout stack = sender as StackLayout;
            View child = stack.Children.Where(c => c.GetType() == typeof(StackLayout)).FirstOrDefault();
            child.IsVisible = !child.IsVisible;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

    }
}
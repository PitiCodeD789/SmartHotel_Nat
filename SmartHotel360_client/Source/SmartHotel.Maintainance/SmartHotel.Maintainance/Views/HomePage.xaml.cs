using SmartHotel.Maintainance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHotel.Maintainance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomePageViewModel vm = new HomePageViewModel();
        public HomePage()
        {
            InitializeComponent();
            BindingContext = vm;
        }


        protected override void OnAppearing()
        {

        }
    }
}


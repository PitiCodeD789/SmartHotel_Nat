using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHotel.Clients.Core.Views.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderItemPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public OrderItemPopup()
        {
            InitializeComponent();
        }
    }
}
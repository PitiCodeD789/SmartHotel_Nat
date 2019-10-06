using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.ViewModels.Base;
using SmartHotel.Services.Hotels.Domain.RoomService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHotel.Clients.Core.ViewModels
{
    class OrderDetailViewModel : ViewModelBase
    {
        public OrderDetailViewModel()
        { 

        }


        private RoomService selectedItem;

        public RoomService SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }

        private List<OrderItem> orderItem;

        public List<OrderItem> OrderItem
        {
            get { return orderItem; }
            set { orderItem = value; OnPropertyChanged(); }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                var navigationParameter = navigationData as Dictionary<string, object>;
                SelectedItem = (RoomService)navigationParameter["SelectItem"];
                OrderItem = SelectedItem.OrderItems;
            }
        }
    }
}

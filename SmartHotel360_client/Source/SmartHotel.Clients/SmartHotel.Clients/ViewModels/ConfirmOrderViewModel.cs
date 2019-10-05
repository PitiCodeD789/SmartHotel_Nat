using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.ViewModels.Base;
using SmartHotel.Services.Hotels.Domain.RoomService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHotel.Clients.Core.ViewModels
{
    class ConfirmOrderViewModel : ViewModelBase
    {
        public ConfirmOrderViewModel()
        {
            OrderItems = App.OrderingCart;
        }
        public List<RestaurantMenuItem> OrderItems { get; set; }

        
    }
}

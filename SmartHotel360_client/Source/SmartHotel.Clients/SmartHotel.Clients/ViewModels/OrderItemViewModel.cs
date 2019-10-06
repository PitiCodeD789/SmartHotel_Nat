using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class OrderItemViewModel: SmartHotel.Services.Hotels.Domain.RoomService.OrderItem
    {
        public decimal Price { get; set; }
    }
}

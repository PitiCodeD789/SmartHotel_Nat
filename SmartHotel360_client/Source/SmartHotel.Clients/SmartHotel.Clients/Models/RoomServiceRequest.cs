using SmartHotel.Services.Hotels.Domain.RoomService;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Core.Models
{
    public class RoomServiceRequest
    {
        public int HotelId { get; set; }
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public string RoomNumber { get; set; }
        public int ServiceTaskType { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}

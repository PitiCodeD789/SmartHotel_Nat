using SmartHotel.Clients.Maintenance.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Maintenance.Models
{
    public class RoomServiceRequest
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public string RoomNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ServiceTaskType { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public bool IsCompleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class RoomServiceViewModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public string RoomNumber { get; set; }
        public int ServiceTaskType { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}

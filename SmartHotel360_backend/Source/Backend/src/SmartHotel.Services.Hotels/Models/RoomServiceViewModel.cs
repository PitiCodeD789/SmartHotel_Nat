using SmartHotel.Services.Hotels.Domain.RoomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Models
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

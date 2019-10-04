using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Domain.RoomService
{
    public class ServiceTask
    {
        [Key]
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string RoomNumber { get; set; }
        public int ServiceTaskType { get; set; }
        public int OrderItemId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

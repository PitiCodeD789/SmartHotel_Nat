using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Domain.RoomService
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int ServiceTaskId { get; set; }
        public int OrderItemId { get; set; }
        public string Item { get; set; }
        public int OrderItemAmount { get; set; }
        public string OrderItemDescription { get; set; }
    }
}

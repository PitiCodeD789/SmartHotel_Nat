using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Maintenance.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ServiceTaskId { get; set; }
        public int OrderItemId { get; set; }
        public int OrderItemAmount { get; set; }
        public string OrderItemDescription { get; set; }
    }
}

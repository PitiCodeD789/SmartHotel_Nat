using SmartHotel.Clients.Maintenance.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Maintenance.Models
{
    public class ServiceTask
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string TaskName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsIncompleted { get { return !IsCompleted; } }
        public List<OrderItem> OrderItems { get; set; }
    }
}

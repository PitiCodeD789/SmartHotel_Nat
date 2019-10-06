using SmartHotel.Services.Hotels.Domain.RoomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHotel.Clients.Core.Models
{
    public class RoomService
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public string RoomNumber { get; set; }
        public int ServiceTaskType { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }


        public string ServiceType
        {
            get { return GetTaskTypeString(); }
        }

        public string DateString
        {
            get { return CreatedDate.ToString("MMM dd HH:mm"); }
        }

        public string ItemCount
        {
            get { return GetItemCount(); }
        }

        public string TotalPrice
        {
            get { return CalTotalPrice(); }
        }

        private string CalTotalPrice()
        {
            decimal total = 0;
            if (OrderItems!=null)
            {
                foreach (var item in OrderItems)
                {
                    total = total + (item.Price * item.OrderItemAmount);
                }
            }
            return total.ToString("C");
        }

        private string GetTaskTypeString()
        {
            if (ServiceTaskType == 1)
            {
                return "FoodService";
            }
            else if(ServiceTaskType == 2)
            {
                return "IceService";
            }
            else if (ServiceTaskType == 3)
            {
                return "ThoothBrushService";
            }
            else if (ServiceTaskType == 4)
            {
                return "TowelService";
            }
            else
            {
                return "MaintenanceService";
            }
        }

        private string GetItemCount()
        {
            if (ServiceTaskType == 1)
            {
                return OrderItems.Count() + " Items";
            }
            else if(ServiceTaskType == 2|| ServiceTaskType == 3 || ServiceTaskType == 4)
            {
                return "1 Item";
            }
            else
            {
                return "NoItem";
            }
        }
    }
}

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


        public string FirstItem
        {
            get { return OrderItems.OrderBy(x=>x.Id).FirstOrDefault().OrderItemDescription; }
        }

        public string DateString
        {
            get { return CreatedDate.ToString("MMM dd HH:mm"); }
        }

        public string ItemCount
        {
            get { return OrderItems.Count()+" Items"; }
        }

        public string TotalPrice
        {
            get { return CalTotalPrice(); }
        }

        private string CalTotalPrice()
        {
            var total = 0;
            foreach (var item in OrderItems)
            {
                total = total + item.OrderItemAmount;
            }
            return total.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Domain.Hotel
{
    public class Menu
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        public int CategoryId { get; set; }
        public bool IsRecommended { get; set; }
        public bool IsAvailable { get; set; }
    }
}

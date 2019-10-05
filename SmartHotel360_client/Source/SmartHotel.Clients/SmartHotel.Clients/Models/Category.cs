using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string CategoryName { get; set; }
    }
}

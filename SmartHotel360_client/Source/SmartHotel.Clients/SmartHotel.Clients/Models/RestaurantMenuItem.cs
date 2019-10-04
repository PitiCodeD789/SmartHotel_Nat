using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Core.Models
{
    public class RestaurantMenuItem
    {
        public int id { get; set; }
        public decimal MunuPrice { get; set; }
        public string ManuName { get; set; }
        public string ManuComment { get; set; }
        public string ManuImg { get; set; }
    }
}

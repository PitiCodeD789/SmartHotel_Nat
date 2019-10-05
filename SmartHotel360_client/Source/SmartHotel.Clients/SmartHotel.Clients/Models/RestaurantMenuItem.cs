using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Core.Models
{
    public class RestaurantMenuItem
    {
        public int id { get; set; }
        public decimal MenuPrice { get; set; }
        public string MenuName { get; set; }
        public string MenuDescription { get; set; }
        public string MenuComment { get; set; }
        public string MenuImg { get; set; }
        public int Amount { get; set; }

        public string TotalItemsPrice
        {
            get { return (MenuPrice*Amount).ToString("N0"); }
        }
        public string AmountX
        {
            get { return Amount+"X"; }
        }


    }
}

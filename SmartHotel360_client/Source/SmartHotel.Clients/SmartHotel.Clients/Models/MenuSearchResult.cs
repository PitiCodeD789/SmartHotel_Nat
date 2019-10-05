using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Core.Models
{
    public class MenuSearchResult
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public bool IsRecommended { get; set; }
        public bool IsAvailable { get; set; }
    }
}

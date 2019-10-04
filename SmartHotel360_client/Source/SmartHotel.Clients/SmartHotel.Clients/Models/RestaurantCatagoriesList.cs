using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Core.Models
{
    public class RestaurantCatagoriesList
    {
        public string CatagoryName { get; set; }
        public bool IsVisble { get; set; }
        public ObservableRangeCollection<RestaurantMenuItem> RestaurantMenuItemList { get; set; }

    }
}

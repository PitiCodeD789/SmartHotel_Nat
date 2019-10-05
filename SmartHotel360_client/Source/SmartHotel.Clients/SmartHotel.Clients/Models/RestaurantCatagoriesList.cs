using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Models
{
    public class RestaurantCatagoriesList
    {
        public string CatagoryName { get; set; }
        public bool IsVisble { get; set; }
        public Command ChangeVisibilityStatusCommand{ get {  return new Command(ChangeVisibilityStatus); } }
        public ObservableRangeCollection<RestaurantMenuItem> RestaurantMenuItemList { get; set; }
        public void ChangeVisibilityStatus()
        {
            IsVisble = !IsVisble;
        }
    }
}

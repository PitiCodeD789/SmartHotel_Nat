
using MvvmHelpers;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class RestaurantViewModel : ViewModelBase
    {
        private RestaurantMenuItem restaurantMenu;
        private ObservableRangeCollection<RestaurantMenuItem> listMenu;
        public RestaurantViewModel()
        {
            ListMenu = new ObservableRangeCollection<RestaurantMenuItem>()
            {
                new RestaurantMenuItem{ id = 0, ManuComment="testAdd" , ManuImg = "FoodImage" , ManuName = "Food Name", MunuPrice = 500 },
                new RestaurantMenuItem{ id = 1, ManuComment="testAdd2" , ManuImg = "FoodImage" , ManuName = "Food Name2", MunuPrice = 400 },
                new RestaurantMenuItem{ id = 2, ManuComment="testAdd3" , ManuImg = "FoodImage" , ManuName = "Food Name3", MunuPrice = 300 },
                new RestaurantMenuItem{ id = 3, ManuComment="testAdd4" , ManuImg = "FoodImage" , ManuName = "Food Name4", MunuPrice = 200 },
                new RestaurantMenuItem{ id = 4, ManuComment="testAdd5" , ManuImg = "FoodImage" , ManuName = "Food Name5", MunuPrice = 100 },
                new RestaurantMenuItem{ id = 5, ManuComment="testAdd6" , ManuImg = "FoodImage" , ManuName = "Food Name6", MunuPrice = 50 }

            };
        }

        public RestaurantMenuItem RestaurantMenu
        {
            get => restaurantMenu;
            set
            {
                if(value != null)
                {
                    OrderItemPopupAsync();
                }
            }
        
           
        
        }

        public ObservableRangeCollection<RestaurantMenuItem> ListMenu
        {
            get => listMenu;
            set => SetProperty(ref listMenu, value);
        }

        Task OrderItemPopupAsync() => NavigationService.NavigateToPopupAsync<OrderItemPopupViewModel>(true);

    }
}

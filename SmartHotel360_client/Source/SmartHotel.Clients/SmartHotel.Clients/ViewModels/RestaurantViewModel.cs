
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
        private ObservableRangeCollection<RestaurantCatagoriesList> catagoriesList;

      


        public RestaurantViewModel()
        {
            ListMenu = new ObservableRangeCollection<RestaurantMenuItem>()
            {
                new RestaurantMenuItem{ id = 0, ManuComment="testAdd" , ManuImg = "Babyfood38" , ManuName = "Food Name", MunuPrice = 500 },
                new RestaurantMenuItem{ id = 1, ManuComment="testAdd2" , ManuImg = "Babyfood39" , ManuName = "Food Name2", MunuPrice = 400 },
                new RestaurantMenuItem{ id = 2, ManuComment="testAdd3" , ManuImg = "Babyfood42" , ManuName = "Food Name3", MunuPrice = 300 },
               
            };

            CatagoriesList = new ObservableRangeCollection<RestaurantCatagoriesList>()
            {
               new RestaurantCatagoriesList{ CatagoryName = "Catagory1", IsVisble = true, RestaurantMenuItemList = ListMenu},
                 new RestaurantCatagoriesList{ CatagoryName = "Catagory2", IsVisble = false,  RestaurantMenuItemList = ListMenu},
                 new RestaurantCatagoriesList{ CatagoryName = "Catagory3", IsVisble = false, RestaurantMenuItemList = ListMenu},

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

        public ObservableRangeCollection<RestaurantCatagoriesList> CatagoriesList
        {
            get => catagoriesList;
            set => SetProperty(ref catagoriesList, value);            
        }

        public ObservableRangeCollection<RestaurantMenuItem> ListMenu
        {
            get => listMenu;
            set => SetProperty(ref listMenu, value);
        }

        Task OrderItemPopupAsync() => NavigationService.NavigateToPopupAsync<OrderItemPopupViewModel>(true);

    }
}

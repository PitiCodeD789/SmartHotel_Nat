
using MvvmHelpers;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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

        public ICommand GoToAddCartCommand => new Command(GoToAddCart);

        private void GoToAddCart(object obj)
        {
            int id = int.Parse(obj.ToString());
            RestaurantMenuItem data = ListMenu.FirstOrDefault(c => c.id == id);
            var navigationParameter = new Dictionary<string, object>
            {
                { "ItemDetail", data.ManuName },
                { "ItemPrice", data.MunuPrice },                
            };
            NavigationService.NavigateToPopupAsync<OrderItemPopupViewModel>(navigationParameter, true);
        }

        public ICommand ShowCatagariesCommand => new Command(ShowCatagaries);

        private void ShowCatagaries(object obj)
        {
            if(obj != null)
            {           
               
                string catName = obj.ToString();
                ObservableRangeCollection<RestaurantCatagoriesList> itemData = new ObservableRangeCollection<RestaurantCatagoriesList>();  
                foreach (RestaurantCatagoriesList item in CatagoriesList)
                {
                    if(item.CatagoryName == catName)
                    {
                        bool isVis = !item.IsVisble;
                        item.IsVisble = isVis;
                        itemData.Add(item);
                    }
                    else
                    {
                        itemData.Add(item);
                    }

                }
                CatagoriesList = itemData;

                OnPropertyChanged("CatagoriesList");
            }
           
        }


        public ObservableRangeCollection<RestaurantCatagoriesList> CatagoriesList
        {
            get => catagoriesList;
            set 
            { if(value != null)
                {
                    SetProperty(ref catagoriesList, value);
                    OnPropertyChanged();

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

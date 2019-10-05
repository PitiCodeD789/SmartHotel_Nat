
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
                new RestaurantMenuItem{ id = 0, MenuComment="testAdd" , MenuImg = "Babyfood38" , MenuName = "Food Name", MenuPrice = 500 },
                new RestaurantMenuItem{ id = 1, MenuComment="testAdd2" , MenuImg = "Babyfood39" , MenuName = "Food Name2", MenuPrice = 400 },
                new RestaurantMenuItem{ id = 2, MenuComment="testAdd3" , MenuImg = "Babyfood42" , MenuName = "Food Name3", MenuPrice = 300 },
               
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
                { "SelectItem", data} ,
                { "RestaurantViewModel", this}
            };
            NavigationService.NavigateToPopupAsync<OrderItemPopupViewModel>(navigationParameter, true);
        }

        public ICommand ShowCatagariesCommand => new Command(ShowCatagaries);

        public ICommand ConfirmOrderCommand => new AsyncCommand(ConfirmOrder);

        private async Task ConfirmOrder()
        {
           await NavigationService.NavigateToAsync<ConfirmOrderViewModel>( true);
        }

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

        public void update()
        {
            var order = App.OrderingCart;
            decimal totalAmounnt = 0;
            foreach(RestaurantMenuItem item in order)
            {
                totalAmounnt = totalAmounnt + (item.MenuPrice * item.Amount);

            }

            int numItem = order.Count;
            if(numItem > 0)
            {
                IsVisbleCart = true;
            DetailInCart = $"Item = {numItem}    Price = {totalAmounnt}";

            }
            else
            {
                IsVisbleCart = false;
            }
            
        }

        private string detailInCart;

        public string DetailInCart
        {
            get 
            { 
                return detailInCart; 
            }
            set
            {
                SetProperty(ref detailInCart, value);
                OnPropertyChanged();
            }
        }

        private bool isVisbleCart;

        public bool IsVisbleCart
        {
            get { return isVisbleCart; }
            set { isVisbleCart = value; OnPropertyChanged(); }
        }

     

    }
}

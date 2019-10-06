
using MvvmHelpers;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Services.Restaurant;
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
      
        private ObservableRangeCollection<RestaurantMenuItem> listMenu;
        private ObservableRangeCollection<RestaurantCatagoriesList> catagoriesList;
        private IRestaurantService restaurantService;

        public RestaurantViewModel()
        {
            restaurantService = new RestaurantService();
        }
        public override async Task InitializeAsync(object navigationData)
        {

            if (App.CatagoriesList == null)
            {

                var res = await restaurantService.GetMenusAsync();
                CatagoriesList = new ObservableRangeCollection<RestaurantCatagoriesList>();
                var recommendedMenu = res.Where(c => c.IsRecommended == true).
                    Select(s => new RestaurantMenuItem()
                    {
                        id = s.Id,
                        Amount = 0,
                        MenuDescription = s.Description,
                        MenuImg = "Babyfood42",

                        MenuName = s.Item,
                        MenuPrice = s.Price
                    }).ToList();
                ObservableRangeCollection<RestaurantMenuItem> restaurant = new ObservableRangeCollection<RestaurantMenuItem>();
                foreach (RestaurantMenuItem item in recommendedMenu)
                {
                    restaurant.Add(item);
                }
                RestaurantCatagoriesList recommendedList = new RestaurantCatagoriesList() { CatagoryName = "Recommended", IsVisble = true, RestaurantMenuItemList = restaurant };
                CatagoriesList.Add(recommendedList);

                var catagoryName = res.Select(c => c.Category.CategoryName).Distinct().ToList();
                foreach (string catName in catagoryName)
                {
                    var catagoriesMenu = res.Where(c => c.Category.CategoryName == catName).
                   Select(s => new RestaurantMenuItem()
                   {
                       id = s.Id,
                       Amount = 0,
                       MenuDescription = s.Description,
                       MenuImg = "Babyfood42",
                       MenuName = s.Item,
                       MenuPrice = s.Price
                   }).ToList();
                    ObservableRangeCollection<RestaurantMenuItem> menuByCatagories = new ObservableRangeCollection<RestaurantMenuItem>();
                    foreach (RestaurantMenuItem item in catagoriesMenu)
                    {
                        menuByCatagories.Add(item);
                    }
                    RestaurantCatagoriesList catList = new RestaurantCatagoriesList() { CatagoryName = catName, IsVisble = false, RestaurantMenuItemList = menuByCatagories };
                    CatagoriesList.Add(catList);

                    //List<RestaurantMenuItem> orderingCart =  
                    var restaurantMenus = res.Select(s => new RestaurantMenuItem()
                    {
                        id = s.Id,
                        Amount = 0,
                        MenuDescription = s.Description,
                        MenuImg = "Babyfood42",
                        MenuName = s.Item,
                        MenuPrice = s.Price
                    }).ToList();
                    App.CatagoriesList = CatagoriesList;
                    App.RestaurantMenus = restaurantMenus;
                }

                
  
            }else
            {
                CatagoriesList = new ObservableRangeCollection<RestaurantCatagoriesList>();
                 
               var  catagories = App.CatagoriesList;
                foreach(RestaurantCatagoriesList list in catagories)
                {
                    CatagoriesList.Add(list);
                }
            }

        }

 

        public ICommand GoToAddCartCommand => new Command(GoToAddCart);

        private void GoToAddCart(object obj)
        {
            int id = int.Parse(obj.ToString());
            List<RestaurantMenuItem> ListMenuMaster = App.RestaurantMenus;
            RestaurantMenuItem data = ListMenuMaster.FirstOrDefault(c => c.id == id);
            var navigationParameter = new Dictionary<string, object>
            {
                { "SelectItem", data} ,
                { "ViewModel", this},
                { "IsConfirmPage",false }
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
            int numItem = 0;
            decimal totalAmounnt = 0;
            foreach(RestaurantMenuItem item in order)
            {
                totalAmounnt = totalAmounnt + (item.MenuPrice * item.Amount);
                numItem = numItem + item.Amount;
            }

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

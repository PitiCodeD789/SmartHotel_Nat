using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.ViewModels.Base;
using SmartHotel.Services.Hotels.Domain.RoomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class OrderItemPopupViewModel : ViewModelBase
    {
        public RestaurantMenuItem SelectedItem { get; set; }
        public RestaurantViewModel restaurantViewModel;
    public OrderItemPopupViewModel()
        {          
          
            TextButton = SetTextButton();
            AddOrderCommand = new Command(AddOrder);
            DeleteOderCommand = new Command(DeleteOrder);
            BgColor = Color.FromHex("#00B14F");
            IsDelete = true;
            IsAdd = false;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                var navigationParameter = navigationData as Dictionary<string, object>;
                SelectedItem = (RestaurantMenuItem)navigationParameter["SelectItem"];
                restaurantViewModel = (RestaurantViewModel)navigationParameter["RestaurantViewModel"];               
                ItemDetail = SelectedItem.MenuName;
                ItemPrice = SelectedItem.MenuPrice;
                TextButton = SetTextButton();
                //if (App.OrderingCart.Where(item => item.id == SelectedItem.id) == null || (App.OrderingCart.Where(item=>item.id == SelectedItem.id)?.Count()) != 0)
                //{
                //    var Item = App.OrderingCart.Where(item => item.id == SelectedItem.id).FirstOrDefault();
                //    Quantity = Item.Amount;
                //    orderComment = Item.MenuComment;
                //    IsAdded = true;
                //}
            }
        }


        private void AddOrder()
        {
            if (IsAdded)
            {
                App.OrderingCart.Where(item => item.id == SelectedItem.id).FirstOrDefault().Amount = Quantity;
                App.OrderingCart.Where(item => item.id == SelectedItem.id).FirstOrDefault().MenuComment = OrderComment;
                restaurantViewModel.update();
            }
            else
            {
                SelectedItem.Amount = Quantity;
                SelectedItem.MenuComment = OrderComment;
                var Orderlist = App.OrderingCart;
                if (Orderlist==null)
                {
                    Orderlist = new List<RestaurantMenuItem>();
                }
                Orderlist.Add(SelectedItem);
                App.OrderingCart = Orderlist;
                restaurantViewModel.update();
            }
        }
        private void DeleteOrder()
        {
            if (IsAdd)
            {
                App.OrderingCart.RemoveAll(item => item.id == SelectedItem.id);
            }
        }

        public virtual ICommand AddOrderCommand { get; set; }
        public virtual ICommand DeleteOderCommand { get; set; }

        public bool IsAdded { get; set; } = false;
        private decimal itemPrice;

        public decimal ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; OnPropertyChanged(); }
        }

        private string itemDetail;

        public string ItemDetail
        {
            get { return itemDetail; }
            set { itemDetail = value; OnPropertyChanged(); }
        }


        private string textButton;

        public string TextButton 
        {
            get { return textButton ; }
            set 
            {
                textButton = value;
                OnPropertyChanged();
            }
        }

        private string orderComment;

        public string OrderComment
        {
            get { return orderComment; }
            set { orderComment = value; OnPropertyChanged(); }
        }


        private string SetTextButton()
        {
            if (Quantity > 0)
            {
                return "Add Order - " + (quantity * ItemPrice).ToString("N0");
            }
            else
            {
                return "Delete";
            }
        }

        private int quantity = 1 ;

        public int Quantity
        {
            get { return quantity; }
            set 
            { 
                quantity = value; 
                OnPropertyChanged();
                if (quantity == 0)
                {
                    BgColor = Color.FromHex("#ff0000");
                    TextButton = SetTextButton();
                    AddOrderCommand = new Command(AddOrder);
                    IsDelete = true;
                    IsAdd = false;
                }
                else
                {
                    BgColor = Color.FromHex("#00B14F");
                    TextButton = SetTextButton();
                    AddOrderCommand = new Command(AddOrder);
                    IsDelete = false;
                    IsAdd = true;
                }
            }
        }

        private Color bgColor;
                
        public Color BgColor
        {
            get { return bgColor; }
            set { bgColor = value; OnPropertyChanged(); }
        }

        private bool isDelete;

        public bool IsDelete
        {
            get { return isDelete; }
            set { isDelete = value; OnPropertyChanged(); }
        }

        private bool isAdd;

        public bool IsAdd
        {
            get { return isAdd; }
            set { isAdd = value; OnPropertyChanged(); }
        }

    }
}

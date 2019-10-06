﻿using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.ViewModels.Base;
using SmartHotel.Services.Hotels.Domain.RoomService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class ConfirmOrderViewModel : ViewModelBase
    {
        public ConfirmOrderViewModel()
        {
            OrderItems = App.OrderingCart;
            TotalPrice = CalTotalPrice();
            EditOrderCommand = new Command<int>(EditOrder);
        }

        public virtual ICommand EditOrderCommand { get; set; }
        public void EditOrder(int id)
        {
            RestaurantMenuItem data = OrderItems.FirstOrDefault(c => c.id == id);

            var navigationParameter = new Dictionary<string, object>
            {
                { "SelectItem", data} ,
                { "ViewModel", this},
                { "IsConfirmPage",true }
            };
            NavigationService.NavigateToPopupAsync<OrderItemPopupViewModel>(navigationParameter, true);
        }

        public void update()
        {
            OrderItems = App.OrderingCart;
            int numItem = 0;
            numItem = OrderItems.Count();
            TotalPrice = CalTotalPrice();

            if (numItem == 0)
            {
               NavigationService.NavigateToAsync<MyRoomViewModel>(true);
            }
        }

        private string totalPrice;

        public string TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; OnPropertyChanged(); }
        }

        private string room;

        public string Room
        {
            get { return room; }
            set { room = value; OnPropertyChanged(); }
        }


        private List<RestaurantMenuItem> orderItems;

        public List<RestaurantMenuItem> OrderItems
        {
            get { return orderItems; }
            set { orderItems = value; OnPropertyChanged();
                ItemList = SetItemList(); }
        }

        private ObservableCollection<RestaurantMenuItem> SetItemList()
        {
            ObservableCollection<RestaurantMenuItem> observItems = new ObservableCollection<RestaurantMenuItem>();
            foreach (var item in OrderItems)
            {
                observItems.Add(new RestaurantMenuItem()
                {
                    id = item.id,
                    MenuName =item.MenuName,
                    Amount = item.Amount,
                    MenuComment = item.MenuComment,
                    MenuPrice =item.MenuPrice,
                    MenuImg = item.MenuImg
                });
            }
            return observItems;
        }

        private ObservableCollection<RestaurantMenuItem> itemList;

        public ObservableCollection<RestaurantMenuItem> ItemList
        {
            get { return itemList; }
            set { itemList = value; OnPropertyChanged(); }
        }


        public string CalTotalPrice()
        {
            decimal Total = 0;
            foreach (RestaurantMenuItem item in OrderItems)
            {
                Total = Total + (item.MenuPrice * item.Amount);
            }
            return Total.ToString("C");
        }

    }
}

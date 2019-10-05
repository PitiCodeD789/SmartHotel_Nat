using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class OrderItemPopupViewModel : ViewModelBase
    {
        
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
                ItemDetail = (string)navigationParameter["ItemDetail"] ;
                ItemPrice = (decimal)navigationParameter["ItemPrice"];
                TextButton = SetTextButton();
            }

            
        }


        private void AddOrder()
        {
            //TODO แอด Item ลง List ของหน้าที่แล้ว
            throw new NotImplementedException();
        }
        private void DeleteOrder()
        {
            //TODO ถ้ามีให้ลบออกจากลิส ถ้าไม่มีก็ไม่ต้องทำอะไร ปิดป็อปอัพไปเฉยๆ
            throw new NotImplementedException();
        }

        public virtual ICommand AddOrderCommand { get; set; }
        public virtual ICommand DeleteOderCommand { get; set; }


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

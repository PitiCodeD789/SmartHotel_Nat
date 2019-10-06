using SmartHotel.Clients.Maintenance.Models;
using SmartHotel.Clients.Maintenance.Services;
using SmartHotel.Clients.Maintenance.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SmartHotel.Clients.Maintenance.ViewModels
{
    public class InputPageViewModel : INotifyPropertyChanged
    {
        public HttpConnectionService _httpConnectionService = new HttpConnectionService();
        public InputPageViewModel()
        {
            buttonVisible = false;
            InputHotel = new Command(InputHotelMethod);
            hotels = GetAllHotel();
        }

        private List<string> GetAllHotel()
        {
            var httpResult = _httpConnectionService.HttpGet<List<HotelModel>>(ServiceEnumerables.Url.GetAllHotel).Result;
            if (httpResult != null)
            {
                int status = (int)httpResult.HttpStatusCode;
                if(status >=200 || status <= 299)
                {
                    hotelDatas = httpResult.Model;
                    List<string> result = hotelDatas.Select(x => x.Name).ToList();
                    return result;
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Error", "ไม่สามารถเชื่อมต่อได้", "OK");
                    return null;
                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "ไม่สามารถเชื่อมต่อได้", "OK");
                return null;
            }
        }

        private List<HotelModel> hotelDatas;
        public List<HotelModel> HotelDatas
        {
            get
            {
                return hotelDatas; 
            }
            set
            {
                hotelDatas = value;
                OnPropertyChanged();
            }
        }

        private List<string> hotels;
        public List<string> Hotels
        {
            get 
            { 
                return hotels; 
            }
            set
            { 
                hotels = value;
                OnPropertyChanged();
            }
        }

        private string selectHotel;
        public string SelectHotel
        {
            get
            {
                return selectHotel; 
            }
            set 
            {
                selectHotel = value;
                CheckValue();
                OnPropertyChanged();
            }
        }

        private bool buttonVisible;
        public bool ButtonVisible
        {
            get
            { 
                return buttonVisible; 
            }
            set 
            {
                buttonVisible = value;
                OnPropertyChanged();
            }
        }

        private void CheckValue()
        {
            if(!String.IsNullOrEmpty(selectHotel))
            {
                ButtonVisible = true;
            }
        }

        public ICommand InputHotel { get; set; }
        public async void InputHotelMethod()
        {
            string select = hotelDatas.Where(x => x.Name == selectHotel).Select(y => y.Id).FirstOrDefault().ToString();
            await SecureStorage.SetAsync("Topic", select);

            MessagingCenter.Send<InputPageViewModel, string>(this, "Topic", select);
            //Environment.Exit(0);
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

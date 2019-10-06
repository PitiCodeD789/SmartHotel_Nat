using Newtonsoft.Json;
using SmartHotel.Clients.Maintenance.Models;
using SmartHotel.Clients.Maintenance.Services;
using SmartHotel.Clients.Maintenance.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
            int selectId = hotelDatas.Where(x => x.Name == selectHotel).Select(y => y.Id).FirstOrDefault();
            string select = selectId.ToString();
            await SecureStorage.SetAsync("Topic", select);

            MessagingCenter.Send<InputPageViewModel, string>(this, "Topic", select);
            CallDeskNotification(selectId, "Server", $"ยินดีตอนรับ {selectHotel} เข้าสู่ระบบ");
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        private void CallDeskNotification(int hotelId, string user, string message)
        {
            try
            {
                var model = new
                {
                    to = "/topics/" + hotelId.ToString(),
                    notification = new
                    {
                        title = user,
                        body = message,
                        click_action = "message"
                    }
                };

                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(20);
                var jsonCommand = JsonConvert.SerializeObject(model);
                HttpContent content = new StringContent(jsonCommand, Encoding.UTF8, "application/json");
                string url = @"https://fcm.googleapis.com/fcm/send";
                string token = @"=AAAASbggBW8:APA91bF5OIUgPZInwsYU8ro0Nmx0NfGGrYcT4Elciwkw_D404Gjt2TclP51qz15VdCKw5FJFT2Ro_TDLVz3lpS1w_6OoySk2dy5wWVzHY5NA2OOKdn24gN0r_De5c9fiACN7lKjoCp3L";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("key", token);
                var result = client.PostAsync(url, content).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
            }

        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

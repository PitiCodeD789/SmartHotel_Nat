using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Services.RoomService;
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
    public class OrderHistoryViewModel:ViewModelBase
    {
        private readonly IRoomServiceService Roomservice;
        public OrderHistoryViewModel()
        {
            Roomservice = new RoomServiceService();
            PendingSelectedCommand = new Command<int>(PendingSelected);
            DeliveredSelectedCommand = new Command<int>(DeliveredSelected);
        }
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                var result = await Roomservice.GetRoomServiceHistoryAsync();
                List<RoomService> CompleteServices = new List<RoomService>();
                List<RoomService> PendingServices = new List<RoomService>();
                foreach (var item in result)
                {
                    if (item.IsCompleted)
                    {
                        CompleteServices.Add(item);
                    }
                    else
                    {
                        PendingServices.Add(item);
                    }
                }
                PendingRoomService = PendingServices;
                DeliveredRoomService = CompleteServices;
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

        private List<RoomService> roomServiceList;

        public List<RoomService> RoomServiceList
        {
            get { return roomServiceList; }
            set { roomServiceList = value; OnPropertyChanged();}  
        }

        private List<RoomService> pendingRoomService;

        public List<RoomService> PendingRoomService
        {
            get { return pendingRoomService; }
            set { pendingRoomService = value; OnPropertyChanged();}
        }

        private List<RoomService> deliveredRoomService;

        public List<RoomService> DeliveredRoomService
        {
            get { return deliveredRoomService; }
            set { deliveredRoomService = value; OnPropertyChanged();}
        }

        public virtual ICommand PendingSelectedCommand { get; set; }
        public virtual ICommand DeliveredSelectedCommand { get; set; }

        public void PendingSelected(int id)
        {
            var selectedItem = PendingRoomService.Where(x => x.Id == id).FirstOrDefault();
            if (selectedItem.ServiceType == "1")
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { "SelectItem", selectedItem} 
                };
                NavigationService.NavigateToAsync<OrderDetailViewModel>(navigationParameter);
            }
        }

        public void DeliveredSelected(int id)
        {
            var selectedItem = DeliveredRoomService.Where(x => x.Id == id).FirstOrDefault();
            if (selectedItem.ServiceType == "1")
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { "SelectItem", selectedItem}
                };
                NavigationService.NavigateToAsync<OrderDetailViewModel>(navigationParameter);
            }
        }

    }
}

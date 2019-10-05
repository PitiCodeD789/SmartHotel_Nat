using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.ViewModels.Base;
using SmartHotel.Services.Hotels.Domain.RoomService;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class OrderHistoryViewModel:ViewModelBase
    {
        public OrderHistoryViewModel()
        {
            
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


    }
}

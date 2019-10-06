using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Maintenance.Services
{
    public class ServiceEnumerables
    {
        public enum Url
        {
            GetRoomServices,
            UpdateServiceTask,
            GetAllHotel
        }

        public enum HttpConnectionError
        {
            CannotRetrieveSecureStorage,
            NotSuccess,
            DeserializeJsonError,
            UnknownError
        }

        public static List<string> UrlString { get; }
            = new List<string>()
            {
                Values.BaseUrl+"RoomService/order/",
                Values.BaseUrl+"RoomService/UpdateServiceTask/",
                Values.BaseUrl+"Hotels",
            };
    }
}

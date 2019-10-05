using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartHotel.Clients.Core.Extensions;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Services.Request;

namespace SmartHotel.Clients.Core.Services.RoomService
{
    public class RoomServiceService : IRoomServiceService
    {
        readonly IRequestService requestService;
        public RoomServiceService()
        {
            requestService = new RequestService();
        }
        public Task<IEnumerable<Models.RoomService>> GetRoomServiceHistoryAsync(string token = "")
        {
            var builder = new UriBuilder(AppSettings.HotelsEndpoint);
            int Hotelid = AppSettings.HotelId;
            Hotelid = 11;
            int bookingId = 1;

            builder.AppendToPath($"roomservice/order/booking/{bookingId}");

            var uri = builder.ToString();

            return requestService.GetAsync<IEnumerable<Models.RoomService>>(uri, token);
        }
    }
}

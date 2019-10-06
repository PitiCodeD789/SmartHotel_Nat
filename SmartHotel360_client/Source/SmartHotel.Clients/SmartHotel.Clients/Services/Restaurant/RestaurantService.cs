using SmartHotel.Clients.Core.Extensions;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHotel.Clients.Core.Services.Restaurant
{
    public class RestaurantService : IRestaurantService
    {
        readonly IRequestService requestService;
        public RestaurantService()
        {
            requestService = new RequestService();
        }

        public Task<StatusMessage> ConfirmOrderAsync(RoomServiceRequest roomServiceRequest, string token = "")
        {
            var builder = new UriBuilder(AppSettings.HotelsEndpoint);
            builder.AppendToPath($"roomservice/order");
            var uri = builder.ToString();
            return requestService.PostAsync<RoomServiceRequest, StatusMessage>(uri, roomServiceRequest, token);
        }

        public Task<IEnumerable<MenuSearchResult>> GetMenusAsync(string token = "")
        {
            var builder = new UriBuilder(AppSettings.HotelsEndpoint);
            int Hotelid = AppSettings.HotelId;
            Hotelid = 11;


            builder.AppendToPath($"roomservice/{Hotelid}/menus");

            var uri = builder.ToString();

            return requestService.GetAsync<IEnumerable<Models.MenuSearchResult>>(uri, token);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHotel.Clients.Core.Services.Restaurant
{
    public interface IRestaurantService
    {
        Task<IEnumerable<Models.MenuSearchResult>> GetMenusAsync(string token = "");

        Task<Models.RoomServiceRequest> ConfirmOrderAsync(Models.RoomServiceRequest roomServiceRequest, string token = "");
    }
}

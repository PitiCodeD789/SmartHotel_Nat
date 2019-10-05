using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHotel.Clients.Core.Services.Restaurant
{
    public interface IRestaurantService
    {
        Task<IEnumerable<Models.MenuSearchResult>> GetMenusAsync(string token = "");

    }
}

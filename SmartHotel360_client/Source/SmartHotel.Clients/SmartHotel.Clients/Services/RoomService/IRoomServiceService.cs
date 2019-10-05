using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartHotel.Clients.Core;

namespace SmartHotel.Clients.Core.Services.RoomService
{
    public interface IRoomServiceService
    {
        
        Task<IEnumerable<List<Models.RoomService>>> GetRoomServiceHistoryAsync(string token = "");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Domain.RoomService
{
    public class Category
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string CategoryName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Domain.RoomService
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string CategoryName { get; set; }
    }
}

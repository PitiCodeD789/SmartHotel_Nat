using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Core.Models
{
    public class UserBooking
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public byte Adults { get; set; }
        public byte Babies { get; set; }
        public byte Kids { get; set; }
        public decimal Price { get; set; }
    }
}

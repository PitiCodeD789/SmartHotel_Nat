﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Models
{
    public class OrderItemViewModel : SmartHotel.Services.Hotels.Domain.RoomService.OrderItem
    {
        public decimal Price { get; set; }
    }
}

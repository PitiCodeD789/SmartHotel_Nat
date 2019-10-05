using Microsoft.EntityFrameworkCore;
using SmartHotel.Services.Hotels.Data;
using SmartHotel.Services.Hotels.Domain.RoomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Queries
{
    public class OrderItemSearchQuery
    {
        private readonly HotelsDbContext _db;
        public OrderItemSearchQuery(HotelsDbContext db) => _db = db;

        public async Task<List<OrderItem>> GetOrderItemsByTaskId(int taskId)
        {
            return await _db.OrderItems.Where(s => s.ServiceTaskId == taskId).OrderBy(s => s.Id).ToListAsync();
        }
    }
}

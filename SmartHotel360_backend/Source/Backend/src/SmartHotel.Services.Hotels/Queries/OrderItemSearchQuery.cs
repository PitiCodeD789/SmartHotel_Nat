using Microsoft.EntityFrameworkCore;
using SmartHotel.Services.Hotels.Data;
using SmartHotel.Services.Hotels.Domain.RoomService;
using SmartHotel.Services.Hotels.Models;
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

        public async Task<List<OrderItemViewModel>> GetOrderItemViewModelByTaskId(int taskId)
        {
            var orderItems = await _db.OrderItems.Where(s => s.ServiceTaskId == taskId).OrderBy(s => s.Id).ToListAsync();
            var orderItemViewModels = new List<OrderItemViewModel>();
            foreach (var item in orderItems)
            {
                orderItemViewModels.Add(new OrderItemViewModel
                {
                    Id = item.Id,
                    Item = item.Item,
                    OrderItemAmount = item.OrderItemAmount,
                    OrderItemDescription = item.OrderItemDescription,
                    OrderItemId = item.OrderItemId,
                    Price = _db.Menus.Where(m => m.Id == item.OrderItemId).FirstOrDefault().Price,
                    ServiceTaskId = item.ServiceTaskId
                });
            }
            return orderItemViewModels;
        }
    }
}

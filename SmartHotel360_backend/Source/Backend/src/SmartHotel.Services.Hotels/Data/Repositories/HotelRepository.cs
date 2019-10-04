using SmartHotel.Services.Hotels.Domain.RoomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Data.Repositories
{
    public class HotelRepository
    {
        private readonly HotelsDbContext _db;

        public HotelRepository(HotelsDbContext db)
        {
            _db = db;
        }

        public void AddTask(List<OrderItem> items,ServiceTask service)
        {
            _db.ServiceTasks.Add(service);
            int serviceId = service.Id;
            foreach (var item in items)
            {
                item.ServiceTaskId = serviceId;
            }
            _db.OrderItems.AddRange(items);
            _db.SaveChangesAsync();
        }
    }
}

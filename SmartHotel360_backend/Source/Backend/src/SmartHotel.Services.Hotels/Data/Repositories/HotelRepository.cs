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
            _db.SaveChanges();
            int serviceId = service.Id;
            foreach (var item in items)
            {
                item.ServiceTaskId = serviceId;
            }
            _db.OrderItems.AddRange(items);
            _db.SaveChanges();
        }

        public bool CompleteTask(int id)
        {
            try
            {
                var task = _db.ServiceTasks.Where(s => s.Id == id).FirstOrDefault();
                task.IsCompleted = true;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

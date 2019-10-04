using Microsoft.EntityFrameworkCore;
using SmartHotel.Services.Hotels.Data;
using SmartHotel.Services.Hotels.Domain.RoomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Queries
{
    public class ServiceTaskSearchQuery
    {
        private readonly HotelsDbContext _db;
        public ServiceTaskSearchQuery(HotelsDbContext db) => _db = db;

        public async Task<IEnumerable<ServiceTask>> GetAllServiceTasksByHotel(int hotelId)
        {
            return await _db.ServiceTasks.Where(s => s.HotelId == hotelId).OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<IEnumerable<ServiceTask>> GetAllServiceTasksByBookingId(int bookingId)
        {
            return await _db.ServiceTasks.Where(s => s.BookingId == bookingId).OrderBy(s => s.Id).ToListAsync();
        }
    }
}

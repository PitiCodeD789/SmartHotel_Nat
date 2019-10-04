using SmartHotel.Services.Hotels.Data.Repositories;
using SmartHotel.Services.Hotels.Domain.Hotel;
using SmartHotel.Services.Hotels.Domain.RoomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Commands
{
    public class RoomServiceRequest
    {
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public string RoomNumber { get; set; }
        public int ServiceTaskType { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }

    public class CreateOrderCommand
    {
        private readonly HotelRepository _hotelRepository;

        public CreateOrderCommand(HotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<bool> Execute(RoomServiceRequest roomServiceRequest)
        {
            var items = roomServiceRequest.OrderItems;
            var service = new ServiceTask()
            {
                BookingId = roomServiceRequest.BookingId,
                RoomNumber = roomServiceRequest.RoomNumber,
                ServiceTaskType = roomServiceRequest.ServiceTaskType
            };
            _hotelRepository.AddTask(items, service);
            return true;
        }
    }
}

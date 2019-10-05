using SmartHotel.Services.Hotels.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Commands
{
    public class UpdateServiceRequest
    {
        public int TaskId { get; set; }
        public string UserId { get; set; }
    }

    public class UpdateServiceCommand
    {
        private readonly HotelRepository _hotelRepository;

        public UpdateServiceCommand(HotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<bool> Execute(UpdateServiceRequest request)
        {
            bool result = _hotelRepository.CompleteTask(request.TaskId);
            return result;
        }
    }
}

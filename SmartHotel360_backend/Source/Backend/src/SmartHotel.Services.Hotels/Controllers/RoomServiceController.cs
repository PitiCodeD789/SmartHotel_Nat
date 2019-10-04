using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHotel.Services.Hotels.Commands;
using SmartHotel.Services.Hotels.Queries;

namespace SmartHotel.Services.Hotels.Controllers
{
    [Route("[controller]")]
    public class RoomServiceController : Controller
    {
        private readonly HotelsSearchQuery _hotelsSearchQuery;
        private readonly MenusSearchQuery _menusSearchQuery;
        private readonly ServiceTaskSearchQuery _serviceTaskSearchQuery;
        private readonly CreateOrderCommand _createOrderCommand;
        

        public RoomServiceController(
            HotelsSearchQuery hotelsSearchQuery,
            MenusSearchQuery menusSearchQuery,
            ServiceTaskSearchQuery serviceTaskSearchQuery,
            CreateOrderCommand createOrderCommand
            )
        {
            _serviceTaskSearchQuery = serviceTaskSearchQuery;
            _createOrderCommand = createOrderCommand;
            _hotelsSearchQuery = hotelsSearchQuery;
            _menusSearchQuery = menusSearchQuery;
        }

        [HttpGet("{hotelId:int}/menus")]
        public async Task<ActionResult> GetMenusByHotel(int hotelId)
        {
            var menus = await _menusSearchQuery.Get(hotelId);

            if (menus == null)
            {
                return NotFound($"Hotel {hotelId} could not be found");
            }

            return Ok(menus);
        }

        [HttpPost("order")]
        //[Authorize]
        public async Task<ActionResult> OrderMenu([FromBody]RoomServiceRequest request)
        {
            //var userId = User.Claims.First(c => c.Type == "emails").Value;
            //if (!string.IsNullOrEmpty(command.UserId) && command.UserId != userId)
            //{
            //    return BadRequest("If userId is used its value must be the logged user id");
            //}
            if (request.BookingId == 0 || request.HotelId == 0 || request.ServiceTaskType == 0 || string.IsNullOrEmpty(request.UserId))
            {
                return BadRequest();
            }
            await _createOrderCommand.Execute(request);
            return Ok();
        }

        [HttpGet("order/{hotelId}")]
        //[Authorize]
        public async Task<ActionResult> GetAllServiceTasksByHotel(int hotelId)
        {
            if (hotelId == 0)
                return BadRequest();
            //var userId = User.Claims.First(c => c.Type == "emails").Value;
            //if (!string.IsNullOrEmpty(command.UserId) && command.UserId != userId)
            //{
            //    return BadRequest("If userId is used its value must be the logged user id");
            //}
            var serviceTasks = await _serviceTaskSearchQuery.GetAllServiceTasksByHotel(hotelId);
            return Ok(serviceTasks);
        }

        [HttpGet("order/booking/{bookingId}")]
        //[Authorize]
        public async Task<ActionResult> GetAllServiceTasksByBookingId(int bookingId)
        {
            if (bookingId == 0)
                return BadRequest();
            //var userId = User.Claims.First(c => c.Type == "emails").Value;
            //if (!string.IsNullOrEmpty(command.UserId) && command.UserId != userId)
            //{
            //    return BadRequest("If userId is used its value must be the logged user id");
            //}
            var serviceTasks = await _serviceTaskSearchQuery.GetAllServiceTasksByBookingId(bookingId);
            return Ok(serviceTasks);
        }
    }
}
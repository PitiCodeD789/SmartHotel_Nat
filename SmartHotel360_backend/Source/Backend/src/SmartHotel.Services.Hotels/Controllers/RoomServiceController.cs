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
        private readonly CreateOrderCommand _createOrderCommand;
        

        public RoomServiceController(
            HotelsSearchQuery hotelsSearchQuery,
            MenusSearchQuery menusSearchQuery,
            CreateOrderCommand createOrderCommand
            )
        {
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
            await _createOrderCommand.Execute(request);
            return Ok();
        }

    }
}
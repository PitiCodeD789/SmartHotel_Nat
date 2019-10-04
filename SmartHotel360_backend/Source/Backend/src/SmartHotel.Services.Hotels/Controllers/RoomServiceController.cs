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
    [Produces("application/json")]
    [Route("api/RoomService")]
    public class RoomServiceController : Controller
    {
        private readonly HotelsSearchQuery _hotelsSearchQuery;
        private readonly MenusSearchQuery _menusSearchQuery;

        public RoomServiceController(
            HotelsSearchQuery hotelsSearchQuery,
            MenusSearchQuery menusSearchQuery
            )
        {
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
        [Authorize]
        public async Task<ActionResult> OrderMenu()
        {
            RoomServiceRequest command = new RoomServiceRequest();
            var userId = User.Claims.First(c => c.Type == "emails").Value;
            if (!string.IsNullOrEmpty(command.UserId) && command.UserId != userId)
            {
                return BadRequest("If userId is used its value must be the logged user id");
            }
            command.UserId = userId;
            return Ok();
        }

    }
}
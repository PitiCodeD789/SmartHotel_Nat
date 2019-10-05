using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHotel.Services.Hotels.Commands;
using SmartHotel.Services.Hotels.Domain.RoomService;
using SmartHotel.Services.Hotels.Models;
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
        private readonly OrderItemSearchQuery _orderItemSearchQuery;
        


        public RoomServiceController(
            HotelsSearchQuery hotelsSearchQuery,
            MenusSearchQuery menusSearchQuery,
            OrderItemSearchQuery orderItemSearchQuery,
            ServiceTaskSearchQuery serviceTaskSearchQuery,
            CreateOrderCommand createOrderCommand
            )
        {
            _orderItemSearchQuery = orderItemSearchQuery;
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
            List<RoomServiceViewModel> serviceTaskList = new List<RoomServiceViewModel>();
            foreach (var task in serviceTasks)
            {
                List<OrderItem> orderItems = new List<OrderItem>(); 
                if (task.ServiceTaskType == 1)
                {
                    orderItems = await _orderItemSearchQuery.GetOrderItemsByTaskId(task.Id);
                }
                serviceTaskList.Add(new RoomServiceViewModel
                {
                    Id = task.Id,
                    BookingId = task.BookingId,
                    HotelId = task.HotelId,
                    ServiceTaskType = task.ServiceTaskType,
                    RoomNumber = task.RoomNumber,
                    OrderItems = orderItems,
                    CreatedDate = task.CreatedDate,
                    IsCompleted = task.IsCompleted
                });
            }
            return Ok(serviceTaskList);
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
            List<RoomServiceViewModel> serviceTaskList = new List<RoomServiceViewModel>();
            foreach (var task in serviceTasks)
            {
                List<OrderItem> orderItems = new List<OrderItem>();
                if (task.ServiceTaskType == 1)
                {
                    orderItems = await _orderItemSearchQuery.GetOrderItemsByTaskId(task.Id);
                }
                serviceTaskList.Add(new RoomServiceViewModel
                {
                    Id = task.Id,
                    BookingId = task.BookingId,
                    HotelId = task.HotelId,
                    ServiceTaskType = task.ServiceTaskType,
                    RoomNumber = task.RoomNumber,
                    OrderItems = orderItems,
                    CreatedDate = task.CreatedDate,
                    IsCompleted = task.IsCompleted
                });

            }
            return Ok(serviceTaskList);
        }
    }
}
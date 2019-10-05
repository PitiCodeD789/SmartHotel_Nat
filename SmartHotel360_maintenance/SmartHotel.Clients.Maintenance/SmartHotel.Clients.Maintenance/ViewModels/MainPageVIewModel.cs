using SmartHotel.Clients.Maintenance.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SmartHotel.Clients.Maintenance.ViewModels
{
    public class MainPageViewModel
    {
        public HttpConnectionService _httpConnectionService = new HttpConnectionService();
        public MainPageViewModel()
        {
            var list = new List<ServiceTaskList>();
            #region TestDataRegion
            //for (int i = 0; i < 4; i++)
            //{
            //    var sList = new ServiceTaskList()
            //    {
            //        new ServiceTask() { RoomNumber = i+"01", TaskName = "Clean Room", IsCompleted = true},
            //        new ServiceTask() { RoomNumber = i+"02", TaskName = "Change Towels", IsCompleted = false },
            //        new ServiceTask() { RoomNumber = i+"03", TaskName = "Room Service", IsCompleted = true }
            //    };
            //    var incompleted = sList.Where(s => s.IsCompleted == false).ToList();
            //    var completed = sList.Where(s => s.IsCompleted == true).ToList(); //TODO: Add orderby datetime
            //    sList.Clear();
            //    sList.AddRange(incompleted);
            //    sList.AddRange(completed);
            //    sList.CreatedOn = DateTime.Now.AddDays(-i).Date.ToString("dddd, MMMM dd, yyyy");
            //    list.Add(sList);
            //}
            #endregion
            var req = GetServiceTasks();
            list.Add(req);
            ListOfTasks = list;
            ListOfTasks = ListOfTasks.OrderByDescending(l => Convert.ToDateTime(l.CreatedOn)).ToList();
            PendingCount = 0;
            List<ServiceTaskList> tempListOfTasks = new List<ServiceTaskList>();
            foreach (var task in ListOfTasks)
            {
                int taskIndex = ListOfTasks.FindIndex(t => t == task);
                tempListOfTasks.Add(new ServiceTaskList
                {
                    CreatedOn = task.CreatedOn,
                });
                PendingCount += task.Count(s => s.IsCompleted == false);
            }
        }

        private ServiceTaskList GetServiceTasks()
        {
            string hotelId = "11";
            var httpResult = _httpConnectionService.HttpGet<List<RoomServiceRequest>>(ServiceEnumerables.Url.GetRoomServices,hotelId).Result;
            var roomService = httpResult.Model;
            ServiceTaskList result = new ServiceTaskList();
            foreach (var item in roomService)
            {
                result.Add(
                    new ServiceTask() { RoomNumber = item.RoomNumber, TaskName = GetTaskName(item.ServiceTaskType), IsCompleted = true }
                );
            }
            return result;
        }

        private string GetTaskName(int taskId)
        {
            switch (taskId)
            {
                case 1: return "Room Service";
                case 2: return "Ice";
                case 3: return "Toothbrush";
                case 4: return "Towels";
                case 5: return "Leaks";
                default: return "";
            }
        }

        private List<ServiceTaskList> _listOfTasks;
        public List<ServiceTaskList> ListOfTasks 
        { 
            get { return _listOfTasks; } 
            set { _listOfTasks = value;} 
        }

        private int _pendingCount;
        public int PendingCount
        {
            get { return _pendingCount; }
            set { _pendingCount = value; }
        }
    }

    public class RoomServiceRequest
    {
        public int HotelId { get; set; }
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public string RoomNumber { get; set; }
        public int ServiceTaskType { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int ServiceTaskId { get; set; }
        public int OrderItemId { get; set; }
        public int OrderItemAmount { get; set; }
        public string OrderItemDescription { get; set; }
    }


    public class ServiceTask
    {
        public string RoomNumber { get; set; }
        public string TaskName { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsIncompleted { get { return !IsCompleted; } }
    }

    public class ServiceTaskList : List<ServiceTask>
    {
        public bool IsToday { get { return (Convert.ToDateTime(CreatedOn) == DateTime.Now.Date); } }
        public string CreatedOn { get; set; }
        public List<ServiceTask> ServiceTasks => this;
    }
}

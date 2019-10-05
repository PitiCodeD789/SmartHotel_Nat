using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SmartHotel.Clients.Maintenance.ViewModels
{
    public class MainPageVIewModel
    {
        public MainPageVIewModel()
        {
            var list = new List<ServiceTaskList>();
            for (int i = 0; i < 4; i++)
            {
                var sList = new ServiceTaskList()
                {
                    new ServiceTask() { RoomNumber = i+"01", TaskName = "Clean Room", IsCompleted = true},
                    new ServiceTask() { RoomNumber = i+"02", TaskName = "Change Towels", IsCompleted = false },
                    new ServiceTask() { RoomNumber = i+"03", TaskName = "Room Service", IsCompleted = true }
                };
                sList.CreatedOn = DateTime.Now.AddDays(-i).Date.ToString("dddd, MMMM dd, yyyy");
                list.Add(sList);
            }

            ListOfTasks = list;
            ListOfTasks = ListOfTasks.OrderByDescending(l => Convert.ToDateTime(l.CreatedOn)).ToList();
            PendingCount = 0;
            foreach (var task in ListOfTasks)
            {
                PendingCount += task.ServiceTasks.Count(s => s.IsCompleted == false);
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

    

    public class ServiceTask
    {
        public string RoomNumber { get; set; }
        public string TaskName { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsIncompleted { get { return !IsCompleted; } }
        public string DisplayName
        {
            get
            {
                return $"{TaskName}, {RoomNumber}";
            }
        }
    }

    public class ServiceTaskList : List<ServiceTask>
    {
        public bool IsToday { get { return (Convert.ToDateTime(CreatedOn) == DateTime.Now.Date); } }
        public string CreatedOn { get; set; }
        public List<ServiceTask> ServiceTasks => this;
    }
}

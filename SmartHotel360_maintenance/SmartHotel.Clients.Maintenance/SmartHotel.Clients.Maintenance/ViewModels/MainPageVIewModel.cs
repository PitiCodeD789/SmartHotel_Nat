using SmartHotel.Clients.Maintenance.Models;
using SmartHotel.Clients.Maintenance.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace SmartHotel.Clients.Maintenance.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public HttpConnectionService _httpConnectionService = new HttpConnectionService();
        private int hotelId { get; set; } = 11;
        public MainPageViewModel()
        {
            UpdateServiceTask();
            DeliverServiceCommand = new Command<ServiceTask>(DeliverService);
        }
        public Command DeliverServiceCommand { get; set; }
        async void DeliverService(ServiceTask task)
        {
            var dialogResult = await Application.Current.MainPage.DisplayAlert("Resolve Task", $"Resolving task {task.TaskName} from room {task.RoomNumber} \n This operation cannot be undone.", "Ok", "Cancel");
            if (dialogResult)
            {
                UpdateServiceRequest request = new UpdateServiceRequest { TaskId = task.Id, UserId = "Username" };
                var result = _httpConnectionService.HttpPost<UpdateServiceRequest>(ServiceEnumerables.Url.UpdateServiceTask, request);
                UpdateServiceTask();
            }
        }

        private ServiceTaskList GetServiceTasks(int hotelId)
        {
            var httpResult = _httpConnectionService.HttpGet<List<RoomServiceRequest>>(ServiceEnumerables.Url.GetRoomServices,hotelId.ToString()).Result;
            var roomService = httpResult.Model;
            ServiceTaskList result = new ServiceTaskList();
            foreach (var item in roomService)
            {
                result.Add(
                    new ServiceTask() 
                    { 
                        Id = item.Id,
                        RoomNumber = item.RoomNumber,
                        TaskName = GetTaskName(item.ServiceTaskType), 
                        IsCompleted = item.IsCompleted,
                        CreatedDate = item.CreatedDate,
                        OrderItems = item.OrderItems
                    }
                );
            }
            return result;
        }
        private void UpdateServiceTask()
        {
            var list = new List<ServiceTaskList>();
            #region TestDataRegion
            var req = GetServiceTasks(hotelId);
            var groupByDateTime = req.GroupBy(r => r.CreatedDate.Date.ToString("dddd, MMMM dd, yyyy")).ToList();
            foreach (var item in groupByDateTime)
            {
                var sList = new ServiceTaskList();
                var incompleted = req.Where(s => s.IsCompleted == false && s.CreatedDate.Date.ToString("dddd, MMMM dd, yyyy") == item.Key).ToList();
                var completed = req.Where(s => s.IsCompleted == true && s.CreatedDate.Date.ToString("dddd, MMMM dd, yyyy") == item.Key).ToList(); //TODO: Add orderby datetime
                sList.Clear();
                sList.AddRange(incompleted);
                sList.AddRange(completed);
                sList.CreatedOn = item.Key;
                list.Add(sList);
            }
            #endregion
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
            set { _listOfTasks = value; OnPropertyChanged(); } 
        }

        private int _pendingCount;
        public int PendingCount
        {
            get { return _pendingCount; }
            set { _pendingCount = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }






}

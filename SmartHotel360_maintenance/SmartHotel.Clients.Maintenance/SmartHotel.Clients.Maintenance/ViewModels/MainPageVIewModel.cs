using SmartHotel.Clients.Maintenance.Models;
using SmartHotel.Clients.Maintenance.Services;
using SmartHotel.Clients.Maintenance.Views;
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
            CompleteDeliveryCommand = new Command<ServiceTask>(CompleteDelivery);
            CompleteMenuDeliveryCommand = new Command(CompleteMenuDelivery);
            RefreshListCommand = new Command(UpdateServiceTask);
        }
        public Command DeliverServiceCommand { get; set; }
        public Command CompleteDeliveryCommand { get; set; }
        public Command CompleteMenuDeliveryCommand { get; set; }
        public Command RefreshListCommand { get; set; }
        async void DeliverService(ServiceTask task)
        {
            if (task.OrderItems.Count > 0 && task.TaskName == GetTaskName(1))
            {
                CurrentOrder = task.OrderItems;
                CurrentTask = task;
                await Application.Current.MainPage.Navigation.PushAsync(new MenuPage(this));
            }
            else
            {
                CompleteDelivery(task);
            }
        }

        public async void CompleteDelivery(ServiceTask task)
        {
            if (task.IsCompleted)
            {
                await Application.Current.MainPage.DisplayAlert("Resolved", "Resolved task cannot be edited", "Ok");
                return;
            }
            var dialogResult = await Application.Current.MainPage.DisplayAlert("Resolve Task", $"Resolving task {task.TaskName} from room {task.RoomNumber} \n This operation cannot be undone.", "Ok", "Cancel");
            if (dialogResult)
            {
                UpdateServiceRequest request = new UpdateServiceRequest { TaskId = task.Id, UserId = "Username" };
                var result = _httpConnectionService.HttpPost<UpdateServiceRequest>(ServiceEnumerables.Url.UpdateServiceTask, request);
                UpdateServiceTask();
            }
        }

        public async void CompleteMenuDelivery()
        {
            var task = CurrentTask;
            var dialogResult = await Application.Current.MainPage.DisplayAlert("Resolve Task", $"Resolving task {task.TaskName} from room {task.RoomNumber} \n This operation cannot be undone.", "Ok", "Cancel");
            if (dialogResult)
            {
                UpdateServiceRequest request = new UpdateServiceRequest { TaskId = task.Id, UserId = "Username" };
                var result = _httpConnectionService.HttpPost<UpdateServiceRequest>(ServiceEnumerables.Url.UpdateServiceTask, request);
                UpdateServiceTask();
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private ServiceTaskList GetServiceTasks(int hotelId)
        {
            var httpResult = _httpConnectionService.HttpGet<List<RoomServiceRequest>>(ServiceEnumerables.Url.GetRoomServices,hotelId.ToString()).Result;
            var roomService = httpResult.Model;
            if (httpResult.IsError)
            {
                Application.Current.MainPage.DisplayAlert("Error", httpResult.Message.ToString(), "Ok");
                return null;
            }
            ServiceTaskList result = new ServiceTaskList();
            foreach (var item in roomService)
            {
                result.Add(
                    new ServiceTask() 
                    { 
                        Id = item.Id,
                        RoomNumber = item.RoomNumber,
                        TaskName = GetTaskName(item.ServiceTaskType), 
                        ImageSource = GetTaskImage(item.ServiceTaskType),
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
            if (req == null)
                return;
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
        private string GetTaskImage(int taskId)
        {
            switch (taskId)
            {
                case 1: return "ic_room_service";
                case 2: return "ic_ice";
                case 3: return "ic_toothbrush_off";
                case 4: return "ic_towel";
                case 5: return "ic_leak";
                default: return "";
            }
        }

        private List<ServiceTaskList> _listOfTasks;
        public List<ServiceTaskList> ListOfTasks 
        { 
            get { return _listOfTasks; } 
            set { _listOfTasks = value; OnPropertyChanged(); } 
        }

        private List<OrderItem> _currentOrder;
        public List<OrderItem> CurrentOrder
        {
            get { return _currentOrder; }
            set { _currentOrder = value; }
        }

        private ServiceTask currentTask;
        public ServiceTask CurrentTask
        {
            get { return currentTask; }
            set { currentTask = value; }
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

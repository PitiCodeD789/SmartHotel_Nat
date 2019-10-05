using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Maintenance.Models
{
    public class ServiceTaskList : List<ServiceTask>
    {
        public bool IsToday { get { return (Convert.ToDateTime(CreatedOn) == DateTime.Now.Date); } }
        public string CreatedOn { get; set; }
        public List<ServiceTask> ServiceTasks => this;
    }
}

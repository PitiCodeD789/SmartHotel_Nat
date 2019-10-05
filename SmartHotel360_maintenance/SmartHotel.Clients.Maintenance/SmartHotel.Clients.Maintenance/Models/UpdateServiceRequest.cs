using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHotel.Clients.Maintenance.Models
{
    public class UpdateServiceRequest
    {
        public int TaskId { get; set; }
        public string UserId { get; set; }
    }
}

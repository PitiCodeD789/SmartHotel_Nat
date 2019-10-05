using SmartHotel.Clients.Maintenance.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SmartHotel.Clients.Maintenance.Models
{
    public class ResultModel<T>
    {
        public T Model { get; set; }
        public ServiceEnumerables.HttpConnectionError Message { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public bool IsError { get; set; }
    }
}

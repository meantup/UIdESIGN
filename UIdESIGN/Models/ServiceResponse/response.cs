using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIdESIGN.Models.ServiceResponse
{
    public class response<T>
    {
        public T result { get; set; }
        public string message { get; set; }
    }
}

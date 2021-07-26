using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.DTO
{
    public class Response<T>
    {
        public T Data { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public string token { get; set; }
    }
}

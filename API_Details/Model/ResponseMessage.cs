﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Model
{
    public class ResponseMessage<T>
    {
        public T Data { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public DynamicParameters param { get; set; }
        public string token { get; set; }
    }
}
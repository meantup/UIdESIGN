using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Model
{
    public class OrderList
    {
        public int id { get; set; }
        public string iname { get; set; }
        public string idesc { get; set; }
        public string icode { get; set; }
        public decimal amount { get; set; }
        public int quantity { get; set; }
        public DateTime tdt { get; set; }
        public string image { get; set; }
    }
    public class OrderList1
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public string ProductCode { get; set; }
        public decimal AmounT { get; set; }
        public int Quantity { get; set; }
        public DateTime TDT { get; set; }
        public string photoImage { get; set; }

    }
    public class Response<T>
    {
        public T Result { get; set; }
        public string message { get; set; }
        
    }
    public class Update
    {
        public string image { get; set; }
        public string iname { get; set; }
        public string idesc { get; set; }
        public string icode { get; set; }
        public decimal amount { get; set; }
        public int quantity { get; set; }
    }
    public class Add
    {
        public string iname { get; set; }
        public string idesc { get; set; }
        public string icode { get; set; }
        public decimal amount { get; set; }
        public int quantity { get; set; }
        public string image { get; set; }
    }
}

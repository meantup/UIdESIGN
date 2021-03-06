using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Core;
using System.Web;

namespace UIdESIGN.Models
{
    public class OrderDetails
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string productName { get; set; }
        [Required]
        public string productDesc { get; set; }
        [Required]
        public string productCode { get; set; }
        [Required]
        public double amounT { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public DateTime tdt { get; set; }
        public string photoImage { get; set; }
    }
    public class Update
    {
        [Required]
        public string productName { get; set; }
        [Required]
        public string productDesc { get; set; }
        [Required]
        public string productCode { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
    public class Add
    {
        [Required]
        public string image { get; set; }
        public string iName { get; set; }
        [Required]
        public string iDesc { get; set; }
        [Required]
        public string iCode { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}

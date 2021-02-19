using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Core;

namespace UIdESIGN.Models
{
    public class OrderDetails
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string iname { get; set; }
        [Required]
        public string idesc { get; set; }
        [Required]
        public string icode { get; set; }
        [Required]
        public double amount { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public DateTime tdt { get; set; }
    }
    public class Update
    {
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

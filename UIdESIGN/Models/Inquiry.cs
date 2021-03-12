using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UIdESIGN.Models
{
    public class Inquiry
    {
        [Required]
        public string startDate { get; set; }
        [Required]
        public string endDate { get; set; }
    }
}

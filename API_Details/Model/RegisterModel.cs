using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Model
{
    public class RegisterModel
    {
        [Required]
        public string _accountName { get; set; }
        [Required]
        public string _accountCode { get; set; }
        [Required]
        [EnumDataType(typeof(Role), ErrorMessage = "Type value is invalid")]
        public Role _type { get; set; } 
    }
}

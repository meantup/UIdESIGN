using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Model
{
    public class AuthModel
    {
        [Required]
        public string apiUserName { get; set; }
        [Required]
        public string apiKey { get; set; } //accountname1234
        [Required]
        public string billerCode { get; set; } //partnercode

        public loginInfo loginInfo { get; set; }
    }
    public class loginInfo
    {
        public string branchCode { get; set; }
        public string eCode { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public DateTime Date { get; set; }
        public string hash { get; set; }
    }
    public class JWTAuthModel
    {
        [Required]
        public string authName { get; set; }
        [Required]
        public string role { get; set; } //accountname1234
        [Required]
        public string branchCode { get; set; } //partnercode
        public string eCode { get; set; }
    }
    public class LoginAccount
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}

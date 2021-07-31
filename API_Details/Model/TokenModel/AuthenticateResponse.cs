using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Model
{
    public class AuthenticateResponse
    {
        public int UserId { get; set; }
        public string LoginId { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public string Email { get; set; }

        public AuthenticateResponse(UserInfo userinfo)
        {
            UserId = userinfo.UserId;
            LoginId = userinfo.LoginId;
            //UserName = userinfo.UserName;
            //UserPass = userinfo.UserPass;
            Email = userinfo.Email;
        }
    }
}

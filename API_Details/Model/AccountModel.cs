using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Model
{
    public class AccountModel
    {
        public class AccountDetail
        {
            public string lname { get; set; }
            public string fname { get; set; }
            public string uname { get; set; }
            public string pass { get; set; }
        }
        public class Login
        {
            public string UserName { get; set; }
            public string UserPass { get; set; }
        }
        public class UserModel
        {
            public string UserName { get; set; }
            public string UserPass { get; set; }
            public string Email { get; set; }
            public string RoleName { get; set; }
            public UserRole UserRole { get; set; }
        }
    }
}

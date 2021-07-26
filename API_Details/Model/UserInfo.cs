using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Model
{
    public class UserInfo
    {
        public string LoginId { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public bool IsActve { get; set; }
    }
}

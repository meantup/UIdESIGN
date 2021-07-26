using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.DTO
{
    public class RefreshToken
    {
        public string token { get; set; }
        public string branchcode { get; set; }

        public string hash { get; set; }

        public string ecode { get; set; }
        public string username { get; set; }
    }
}

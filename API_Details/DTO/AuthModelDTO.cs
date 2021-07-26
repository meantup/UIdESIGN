using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.DTO
{
    public class AuthModelDTO
    {
        public string apiUserName { get; set; }

        public string apiKey { get; set; } //accountname1234

        public string billerCode { get; set; } //partnercode

        public dtologinInfo loginInfo { get; set; }
    }
    public class dtologinInfo
    {
        public string branchCode { get; set; }
        public string eCode { get; set; }
        public string userName { get; set; }
        public string Date { get; set; }
        public string hash { get; set; }
    }
}

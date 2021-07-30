using API_Details.Helper;
using API_Details.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Class
{
    [Controller]
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        public AccountModel.UserModel _user => (AccountModel.UserModel)HttpContext.Items["APIUser"];
    }
}

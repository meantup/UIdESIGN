using API_Details.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Helper
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute,IAuthorizationFilter
    {
        private readonly IList<Role> _roles;
        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] { };
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ResponseMessage<string> response = new ResponseMessage<string>();

            var user = (AccountModel.UserModel)context.HttpContext.Items["APIUser"];
            var token_expired = (bool)context.HttpContext.Items["expired_token"];
            if (token_expired)
            {
                response.message = "Expired Token";
                response.code = StatusCodes.Status401Unauthorized;
                // not logged in or role not authorized
                context.Result = new JsonResult(new { response }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else if (user == null)
            {
                response.message = "Credential not validated";
                response.code = StatusCodes.Status401Unauthorized;
                // not logged in or role not authorized
                context.Result = new JsonResult(new { response }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else if (_roles.Any() && !_roles.Contains(user.UserRole))
            {
                response.message = "Access not allowed";
                response.code = StatusCodes.Status401Unauthorized;
                // not logged in or role not authorized
                context.Result = new JsonResult(new { response }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}

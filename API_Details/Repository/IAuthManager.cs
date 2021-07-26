using API_Details.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Repository
{
    public interface IAuthManager
    {
        ResponseMessage<string> GenerateJwt(AccountModel.UserModel model);
        ResponseMessage<object> RegisterUser(RegisterModel user);
        Task<ResponseMessage<UserInfo>> Validate(string username, string password);
    }
}

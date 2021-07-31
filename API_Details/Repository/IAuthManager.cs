using API_Details.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Repository
{
    public interface IAuthManager
    {
        TokenRequest GenerateJwt(AccountModel.UserModel model);
        ResponseMessage<object> RegisterUser(RegisterModel user);
        UserInfo GetById(int id);
        Task<ResponseToken<TokenRequest>> Validate(string username, string password);
    }
}

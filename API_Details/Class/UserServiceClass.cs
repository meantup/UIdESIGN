using API_Details.Model;
using API_Details.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Class
{
    public class UserServiceClass : IUserService
    {
        public Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<UserInfo> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<UserInfo> SelectUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}

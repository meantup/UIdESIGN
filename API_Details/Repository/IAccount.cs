using API_Details.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Repository
{
    public interface IAccount
    {
        Task<Response<dynamic>> createAccount(AccountModel.AccountDetail account);
        Task<Response<dynamic>> OpenAccount(string username, string password);
    }
}

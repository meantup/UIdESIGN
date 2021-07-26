using API_Details.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Model
{
    public class AccountClass : IAccount
    {
        private readonly IConfiguration _config;
        private SqlConnection _connection;
        public AccountClass(IConfiguration config)
        {
            _config = config;
            _connection = new SqlConnection(_config["ConnectionStrings:AccountConnection"]);
        }
        public async Task<Response<dynamic>> createAccount(AccountModel.AccountDetail account)
        {
            var res = new Response<dynamic>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("type", "register");
                var getdetail = account.GetType().GetProperties();
                foreach (var item in getdetail)
                {
                    var name = item.Name;
                    var value = item.GetValue(account);
                    param.Add(name, value);
                }
                var queryResult = await _connection.QueryAsync<SqlResponse>("usp_AccountDetails", param, commandType: CommandType.StoredProcedure);
                var queryList = queryResult.ToList();
                res.Result = queryList[0].Result.ToString();
                res.message = queryList[0].Msg.ToString();
            }
            catch (SqlException sql)
            {
                res.message = sql.Message;
                res.Result = 501;
            }
            catch (Exception ee)
            {
                res.message = ee.Message;
                res.Result = 500;
            }
            return res;
        }

        public async Task<Response<dynamic>> OpenAccount(string username, string password)
        {
            var res = new Response<dynamic>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("type", "login");
                param.Add("pass", password);
                param.Add("uname", username);

                var queryResult = await _connection.QueryAsync<SqlResponse>("usp_AccountDetails", param, commandType: CommandType.StoredProcedure);
                var queryList = queryResult.ToList();
                res.Result = queryList[0].Result.ToString();
                res.message = queryList[0].Msg.ToString();
            }
            catch (SqlException sql)
            {
                res.message = sql.Message;
                res.Result = 501;
            }
            catch (Exception ee)
            {
                res.message = ee.Message;
                res.Result = 500;
            }
            return res;
        }
    }
}

using API_Details.Model;
using API_Details.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API_Details.Class
{
    public class AuthManagerClass : IAuthManager
    {
        private readonly IConfiguration configuration;
        private SqlConnection connection;
        public AuthManagerClass(IConfiguration _configuration)
        {
            configuration = _configuration;
            connection = new SqlConnection(configuration["ConnectionStrings:AccountConnection"]);
        }
        public ResponseMessage<string> GenerateJwt(AccountModel.UserModel model)
        {
            ResponseMessage<string> res = new ResponseMessage<string>();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthManager:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
           
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, model.sub),
                new Claim("roles", model.LoginId),
                //new Claim("username", model.UserName),
                //new Claim("password", model.UserPass),
                new Claim("date", DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(configuration["AuthManager:ValidIssuer"],
            configuration["AuthManager:ValidAudience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);


            res.Data = new JwtSecurityTokenHandler().WriteToken(token);
            res.code = 200;
            res.message = "Token Generated";
            return res;

        }
        public ResponseMessage<object> RegisterUser(RegisterModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseMessage<UserInfo>> Validate(string username, string password)
        {
            var reponseMsg = new ResponseMessage<UserInfo>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("user", username);
                param.Add("pass", password);
                param.Add("retval", DbType.Int32, direction: ParameterDirection.Output);
                var res = await connection.QueryAsync<UserInfo>("usp_usercredAPI", param, commandType: CommandType.StoredProcedure);
                int retval = param.Get<int>("retval");
                if (retval.Equals(200))
                {
                    var ret = res.ToList();
                    AccountModel.UserModel model = new AccountModel.UserModel();
                    model.Email = ret[0].Email.ToString();
                    model.LoginId = ret[0].LoginId.ToString();
                    model.UserName = ret[0].UserName.ToString();
                    model.UserPass = ret[0].UserPass.ToString();
                    model.sub = ret[0].sub.ToString();

                    reponseMsg.token = GenerateJwt(model).Data;
                    reponseMsg.code = 200;
                    reponseMsg.message = "success";

                }
                else
                {
                    reponseMsg.message = "Error In Authenticate Users in Claiming Token!";
                    reponseMsg.token = null;
                    reponseMsg.param = null;
                }
            }
            catch (SqlException sql)
            {
                reponseMsg.message = sql.Message;
            }
   
            return reponseMsg;
        }
    }
}

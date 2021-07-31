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
        private readonly List<UserInfo> user = new List<UserInfo>();
        public AuthManagerClass(IConfiguration _configuration)
        {
            configuration = _configuration;
            connection = new SqlConnection(configuration["ConnectionStrings:AccountConnection"]);
        }
        public TokenRequest GenerateJwt(AccountModel.UserModel model)
        {
            var res = new TokenRequest();
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthManager:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var tokenHandler = new JwtSecurityTokenHandler();
                res.token_expire = 1800;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", model.UserId.ToString()), new Claim("roles", model.LoginId), new Claim("email", model.Email), new Claim(JwtRegisteredClaimNames.Sub, model.sub), new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) }),
                    Expires = DateTime.UtcNow.AddSeconds(1800),
                    SigningCredentials = credentials
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                res.token = tokenHandler.WriteToken(token);
                res.access_type = "Bearer";
              
            }
            catch (Exception ee)
            {
                res.access_type = ee.Message;
            }
            return res;
        }

        public UserInfo GetById(int id)
        {
            var resquest = new List<UserInfo>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("type", "select");
                param.Add("id", id);
                param.Add("retval", DbType.Int32, direction: ParameterDirection.Output);
                var res = connection.Query<UserInfo>("usp_usercredAPI", param, commandType: CommandType.StoredProcedure);
                int retval = param.Get<int>("retval");
                if (retval.Equals(200))
                {
                    resquest = res.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resquest.FirstOrDefault();
        }

        public ResponseMessage<object> RegisterUser(RegisterModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseToken<TokenRequest>> Validate(string username, string password)
        {
            var reponseMsg = new ResponseToken<TokenRequest>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("user", username);
                param.Add("type", "getuserlist");
                param.Add("pass", password);
                param.Add("retval", DbType.Int32, direction: ParameterDirection.Output);
                var res = await connection.QueryAsync<AccountModel.UserModel>("usp_usercredAPI", param, commandType: CommandType.StoredProcedure);
                int retval = param.Get<int>("retval");
                if (retval.Equals(200))
                {
                    var ret = res.ToList();
                    AccountModel.UserModel model = new AccountModel.UserModel();
                    model.UserId = Convert.ToInt32(ret[0].UserId.ToString());
                    model.Email = ret[0].Email.ToString();
                    model.LoginId = ret[0].LoginId.ToString();
                    model.UserName = ret[0].UserName.ToString();
                    model.UserPass = ret[0].UserPass.ToString();
                    model.sub = ret[0].sub.ToString();
                    reponseMsg.Data = GenerateJwt(model);
         
                }
            }
            catch (SqlException sql)
            {
                reponseMsg.Data = null;
            }
   
            return reponseMsg;
        }
    }
}

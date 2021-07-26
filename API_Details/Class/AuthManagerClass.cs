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

            //claim is used to add identity to JWT token
            //var claims = new[] {
            //    new Claim(JwtRegisteredClaimNames.Sub, auth.uname),
            //    //new Claim(JwtRegisteredClaimNames.Email, user.Email),
            //    new Claim("roles", "admin"),
            //    new Claim("username", auth.uname),
            //    new Claim("password", auth.pass),
            //    new Claim("Date", DateTime.Now.ToString()),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //};

            var header = new JwtHeader(credentials); 
            DateTime expiry = DateTime.Now.AddMinutes(5);
            var intExp = expiry.Second;
            //int ts = (int)(expiry - new DateTime(2021, 7, 26)).TotalSeconds;

            var payload = new JwtPayload {
                { "sub", configuration["AuthManager:Issuer"]},
                { "name", configuration["AuthManager:Issuer"]},
                { "email", "markocariza@gmail.com"},
                { "exp", intExp},
                { "iis", "http://192.168.210.165/" },
                { "aud", "http://localhost:51917/" }
            };

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            //var token = new JwtSecurityToken(configuration["AuthManager:Issuer"],
            //  configuration["AuthManager:Issuer"],
            //  expires: DateTime.Now.AddMinutes(5),
            //  signingCredentials: credentials);
            var tokenString = handler.WriteToken(secToken);
            res.Data = tokenString;
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
                param.Add("retval", direction: ParameterDirection.Output);
                var res = await connection.QueryAsync("usp_usercredAPI", param, commandType: CommandType.StoredProcedure);
                int retval = param.Get<int>("retval");
                if (retval.Equals(200))
                {
                    AccountModel.UserModel model = new AccountModel.UserModel();

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

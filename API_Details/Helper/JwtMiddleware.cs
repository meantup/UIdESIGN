using API_Details.Model;
using API_Details.Repository;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Details.Helper
{
    public class JwtMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        private IJsonSerializer _serializer = new JsonNetSerializer();
        private IDateTimeProvider _provider = new UtcDateTimeProvider();
        private IBase64UrlEncoder _urlEncoder = new JwtBase64UrlEncoder();
        private IJwtAlgorithm _algorithm = new HMACSHA256Algorithm();

        public JwtMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext context, IAuthManager userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            context.Items["expired_token"] = false;
            if (token != null)
                attachUserToContext(context, userService, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IAuthManager userService, string token)
        {
            try
            {
                IJwtValidator _validator = new JwtValidator(_serializer, _provider);
                IJwtDecoder decoder = new JwtDecoder(_serializer, _validator, _urlEncoder, _algorithm);
                var tokenExpr = decoder.DecodeToObject<JwtToken>(token);
                DateTimeOffset dtOffset = DateTimeOffset.FromUnixTimeSeconds(tokenExpr.expire);
                var tokenExpired = dtOffset.LocalDateTime;

                if (DateTime.Now > tokenExpired)
                {
                    context.Items["expired_token"] = true;
                }
                else
                {
                    context.Items["expired_token"] = false;

                    var tokenHandler = new JwtSecurityTokenHandler();
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthManager:Key"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 30 minutes later)
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                    UserInfo user = new UserInfo();
                    user.UserId = userId;
                    user.LoginId = jwtToken.Claims.First(x => x.Type == "roles").Value;
                    user.Email = jwtToken.Claims.First(x => x.Type == "email").Value;
                    user.sub = jwtToken.Claims.First(x => x.Type == "sub").Value;

                    // attach user to context on successful jwt validation
                    context.Items["User"] = user;
                } 
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}

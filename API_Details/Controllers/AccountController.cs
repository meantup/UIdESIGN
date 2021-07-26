using API_Details.Model;
using API_Details.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public ILogger<AccountController> _logger { get; }
        private readonly IAdapterRepository _repo;
        private readonly IAuthManager _auth;
        private readonly IMapper _mapper;

        public AccountController(IAdapterRepository repo, IAuthManager auth, IMapper mapper, ILogger<AccountController> logger)
        {
            _repo = repo;
            _auth = auth;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult login(string username,string password)
        {
            AccountModel.UserModel usermodel = new AccountModel.UserModel();
            usermodel.UserName = username;
            usermodel.UserPass = password;
            //IAccountResult response = Unauthorized();
            var res = _auth.Validate(username, password);

            return Ok(res);
        }
        [HttpGet]
        [Route("NewAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountModel.AccountDetail account)
        {
            var res = await _repo.account.createAccount(account);
            return Ok(res);
        }
        [HttpGet]
        [Route("OpenAccount/{username}/{password}")]
        public async Task<IActionResult> OpenAccount(string username,string password)
        {
            var res = await _repo.account.OpenAccount(username, password);
            return Ok(res);
        }
    }
}

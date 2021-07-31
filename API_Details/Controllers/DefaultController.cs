using API_Details.Class;
using API_Details.Model;
using API_Details.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DefaultController : BaseController
    {
        private readonly IAdapterRepository _repository;
        private readonly IWebHostEnvironment _webhost;

        public DefaultController(IAdapterRepository repository,IWebHostEnvironment webhost)
        {
            _repository = repository;
            _webhost = webhost;
        }
        [HttpGet("Inquiry")]
        public async Task<IActionResult> OrderList(string startDate, string endDate)
        {
            var clist = await _repository.repos.postItem(startDate,endDate);
            
            return Ok(clist);
        }
        [HttpGet("Remove")]
        public async Task<IActionResult> RemoveItem(int id)
        {
            var retval = await _repository.repos.RemoveRecord(id);
            return Ok(retval);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateItem(int id, Update listup)
        {
            var res = await _repository.repos.UpdateData(id, listup);
            return Ok(res);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> ADD_data(Add add)
        {
            var res = await _repository.repos.DataADD(add);
            return Ok(res);
        }
        [HttpGet("SelectAll")]
        public async Task<IActionResult> loadAll()
        {
            var res = await _repository.repos.selectAll();
            return Ok(res);
        }   
    }
}

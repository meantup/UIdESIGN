using API_Details.Model;
using API_Details.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DefaultController : ControllerBase
    {
        private readonly IRepository _repository;

        public DefaultController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("Inquiry")]
        public async Task<IActionResult> OrderList(string startDate, string endDate)
        {
            List<OrderList1> list = new List<OrderList1>();
            list = await _repository.postItem(startDate,endDate);
            return Ok(list);
        }
        [HttpGet("Remove")]
        public async Task<IActionResult> RemoveItem(int id)
        {
            var retval = new Response<int>();
            retval = await _repository.RemoveRecord(id);
            return Ok(retval);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateItem(int id, Update listup)
        {
            var res = new Response<int>();
            res = await _repository.UpdateData(id, listup);
            return Ok(res);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> ADD_data(Update add)
        {
            var res = new Response<int>();
            res = await _repository.DataADD(add);
            return Ok(res);
        }
    }
}

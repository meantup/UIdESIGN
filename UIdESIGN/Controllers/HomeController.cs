using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UIdESIGN.Models;
using UIdESIGN.Models.ServiceResponse;

namespace UIdESIGN.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Class.APIclass api = new Class.APIclass();

        private readonly IHostingEnvironment env;

        public HomeController(IHostingEnvironment _env)
        {
            env = _env;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult OrderDetails()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Order(Inquiry inQui)
        {
            List<OrderDetails> list = new List<OrderDetails>();
            DataTable dt = new DataTable();
            try
            {
                if (ModelState.IsValid)
                {
                    var retval = api.Details(inQui);
                    var res = JsonConvert.DeserializeObject<response<List<OrderDetails>>>(retval);
                    var data = JsonConvert.SerializeObject(res.result);
                    dt = (DataTable)JsonConvert.DeserializeObject(data, typeof(DataTable));
                    if (res.message.Equals("Successful"))
                    {
                        list = dt.AsEnumerable().Select(x => new OrderDetails
                        {
                            id = int.Parse(x["id"].ToString()),
                            productName = x["ProductName"].ToString(),
                            productDesc = x["ProductDesc"].ToString(),
                            productCode = x["ProductCode"].ToString(),
                            amounT = double.Parse(x["amounT"].ToString()),
                            quantity = int.Parse(x["quantity"].ToString()),
                            tdt = DateTime.Parse(x["tdt"].ToString())
                        }).ToList();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Json(list);
        }
        [HttpGet]
        public IActionResult Remove(int id)
        {
            string _message = string.Empty;
            bool _isSuccess = false;
            var response = api.RemoveItem(id);
            _message = response.Contains(Convert.ToString(1)) ? "Successful Deleted!" : "Unsuccessful Deleted";
            _isSuccess = _message.Equals("Successful Deleted!") ? true : false;
            return Json(new { msg = _message, isSuccess = _isSuccess});
        }
        [HttpPost]
        public IActionResult Update(int id,Update upd)
        {
            bool _iSsuccess = true;
            string _msg = string.Empty;
            var retval = api.UpdateData(id, upd);
            _msg = retval.Contains(Convert.ToString(1)) ? "Successfully Updated!" : "Unsuccessful Updated!";
            _iSsuccess = _msg.Equals("Unsuccessful Updated!") ? false : true;
            return Json(new { isSuccess = _iSsuccess, msg = _msg });
        }
        [HttpGet]
        public IActionResult AddItem()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Add itm)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { });
        }
    }
}

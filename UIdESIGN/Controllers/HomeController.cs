using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using UIdESIGN.Class;
using UIdESIGN.Models;
using UIdESIGN.Models.ServiceResponse;
using UIdESIGN.Repository;

namespace UIdESIGN.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAdapterRepository _adapter = new Adapter();

        //public HomeController(IAdapterRepository adapter)
        //{
        //    _adapter = adapter;
        //}

        private readonly IWebHostEnvironment env;

        public HomeController(IWebHostEnvironment _env)
        {
            env = _env;
        }
        public IActionResult Index()
        {
            var order = new List<Add>();
            try
            {
                var res = _adapter.api.PostData();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(res);
                if (dt.Rows.Count > 0)
                {
                    order = dt.AsEnumerable().Select(x => new Add
                    {
                        image = x["photoImage"].ToString(),
                        iDesc = x["productDesc"].ToString(),
                        iName = x["productName"].ToString(),
                        iCode = x["productCode"].ToString(),
                        Amount = Double.Parse(x["amounT"].ToString()),
                        Quantity = int.Parse(x["quantity"].ToString())
                    }).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return View(order);
        }
        public IActionResult sample()
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
            var list = new List<OrderDetails>();
            //var response = new response<List<OrderDetails>>();
            DataTable dt = new DataTable();
            try
            {
                if (ModelState.IsValid)
                {
                    var retval = _adapter.api.Details(inQui);
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
                            tdt = DateTime.Parse(x["tdt"].ToString()),
                            photoImage = x["photoImage"].ToString()   
                        }).ToList();
                    }
                }
            }
            catch (Exception)
            {
                return Json(new List<OrderDetails>());
            }
            return Json(list);
        }
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var response = new response<int>();
            try
            {
                var res = _adapter.api.RemoveItem(id);
                var retval = JsonConvert.DeserializeObject<response<int>>(res);
                response.message = res.Contains(Convert.ToString(1)) ? "Successful Deleted!" : "Unsuccessful Deleted";
                response.isSuccess = response.message.Equals("Successful Deleted!") ? true : false;
                response.result = response.isSuccess.Equals(true) ? 100 : 101;
            }
            catch (Exception)
            {
                response.message = "Internal Code Error, Please Contact Your Developer!";
                response.isSuccess = false;
                response.result = 404;
            }
            return Json(new { msg = response.message, isSuccess = response.isSuccess , result = response.result});
        }
        [HttpPost]
        public IActionResult Update(int id,Update upd)
        {
            var response = new response<int>();
            try
            {
                var retval = _adapter.api.UpdateData(id, upd);
                var res = JsonConvert.DeserializeObject<response<int>>(retval);
                response.message = res.result.Equals(1) ? "Successfully Updated!" : "Unsuccessful Updated!";
                response.isSuccess = response.message.Equals("Unsuccessful Updated!") ? false : true;
                response.result = response.isSuccess.Equals(true) ? 1 : 0;
            }
            catch (Exception)
            {
                response.message = "Internal Code Error, Please Contact Your Developer!";
                response.isSuccess = false;
                response.result = 404;
            } 
            return Json(new { isSuccess = response.isSuccess, msg = response.message , result = response.result});
        }
        [HttpGet]
        public IActionResult AddItem()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Add itm)
        {
            var response = new response<int>();
            try
            {
                //itm.image = await SaveImage(itm.formFile);
                var val = _adapter.api.addProduct(itm);
                var retval = JsonConvert.DeserializeObject<response<int>>(val);
                response.message = retval.result.Equals(100) ? "Successful" : "Unsuccesful";
                response.isSuccess = response.message.Equals("Successful") ? true : false;
                response.result = response.isSuccess.Equals(true) ? 100 : 101;
            }
            catch (Exception ee)
            {
                response.result = 404;
                response.isSuccess = false;
                response.message = "Internal Code Error, Please Contact Your Developer!";
            }
            return Json(new { isSuccess = response.isSuccess, msg = response.message, response = response.result });
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile ImageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(ImageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(ImageFile.FileName);
            var imagePath = Path.Combine(env.ContentRootPath, "Image", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
    }
}

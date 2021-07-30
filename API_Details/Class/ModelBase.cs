using API_Details.Helper;
using API_Details.Repository;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Model
{
    public class ModelBase : IRepository
    {
        private readonly IMapper _mapper;
        private SqlConnection _connection;
        private readonly IConfiguration _configuration;
        public ModelBase(IMapper mapper,IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
            _connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        }

        public async Task<Response<dynamic>> DataADD(Add add)
        {
            var res = new Response<dynamic>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("retval", DbType.Int32, direction: ParameterDirection.Output);
                var objects = add.GetType().GetProperties();
                foreach (var item in objects)
                {
                    var name = item.Name;
                    var value = item.GetValue(add);
                    param.Add(name, value);
                       
                }
                await _connection.ExecuteAsync("usp_InsertItemOrder",param,commandType:CommandType.StoredProcedure);
                res.message = param.Get<int>("retval") == 100 ? "Successful" : "Unsuccessful";
                res.Result = res.message == "Successful" ? 100 : 101;
            }
            catch (Exception ee)
            {
                res.Result = 500;
                res.message = ee.Message;
            }
            return res;
        }

        public async Task<Response<List<OrderList1>>> postItem(string startDate, string endDate)
        {
            var response = new Response<List<OrderList1>>();
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("retval", SqlDbType.Int ,direction: ParameterDirection.Output);
                param.Add("from",startDate,DbType.DateTime);
                param.Add("to", endDate, DbType.DateTime);
                var ss = _connection.Query<OrderList>("usp_InquiryDate", param, commandType: CommandType.StoredProcedure).AsList();
                var res = await Task.Run(()=> _mapper.Map<List<OrderList1>>(ss));
                var serial = JsonConvert.SerializeObject(res);
                response.message = param.Get<int>("retval") == 100 ? "Successful" : "No Data Found!";
                response.Result = res;
                return response;
            }
            catch (Exception ee)
            {
                response.message = ee.Message;
            }
            return response;
        }
        public async Task<Response<dynamic>> RemoveRecord(int id)
        {
            var respnse = new Response<dynamic>();
            try
            {
                DynamicParameters param_ = new DynamicParameters();
                param_.Add("retval", DbType.Int32,direction:ParameterDirection.Output);
                param_.Add("id", id);
                await _connection.ExecuteAsync("usp_removeItem ", param_,commandType:CommandType.StoredProcedure);
                respnse.message = param_.Get<int>("retval") == 1 ? "Successful"  : "Unsuccessful";
                respnse.Result = respnse.message == "Successful" ? 1 : 0;
            }
            catch (Exception ee)
            {
                respnse.Result = 2;
                respnse.message = ee.Message;
            }
            return respnse;
        }
            
        public async Task<List<OrderList1>> selectAll()
        {            
            try
            {
                var ss = _connection.Query<OrderList>("loadAddProduct", commandType: CommandType.StoredProcedure).ToList();
                var res = await Task.Run(() => _mapper.Map<List<OrderList1>>(ss));
                return res;
            }
            catch (Exception)
            {
                return new List<OrderList1>();
            }
        }

        public async Task<Response<dynamic>> UpdateData(int id, Update listupd)
        {
            var response = new Response<dynamic>();
            try
            {
                DynamicParameters dynamic_ = new DynamicParameters();
                dynamic_.Add("retval", DbType.Int32, direction: ParameterDirection.Output);
                dynamic_.Add("id", id,DbType.Int32);
                var objects = listupd.GetType().GetProperties();
                foreach (var item in objects)
                {
                    var name = item.Name;
                    var value = item.GetValue(listupd);
                    dynamic_.Add(name, value);
                }
                await _connection.ExecuteAsync("usp_UpdateItem", param: dynamic_, commandType: CommandType.StoredProcedure);
                int res = dynamic_.Get<int>("retval");
                response.message = res == 1 ? "Successful" : "Unsuccessful";
                response.Result = response.message == "Successful" ? 1 : 0;
            }
            catch (Exception ee)
            {
                response.Result = 500;
                response.message = ee.Message;
            }
            return response;
        }
    }
}

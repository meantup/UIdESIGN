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
    public class ModelBase<T> : IRepository
    {
        private readonly IMapper _mapper;
        public ModelBase(IMapper mapper)
        {
            _mapper = mapper;
        }
        public string ConnectionString()
        {
            try
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var ss = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
                return ss;
            }
            catch (Exception)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            }
        }

        public async Task<Response<int>> DataADD(Add add)
        {
            var res = new Response<int>();
            var conn = ConnectionString();
            try
            {
                using (var con = new SqlConnection(conn))
                {
                    con.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("retval", DbType.Int32, direction: ParameterDirection.Output);
                    var objects = add.GetType().GetProperties();
                    foreach (var item in objects)
                    {
                        var name = item.Name;
                        var value = item.GetValue(add);
                        param.Add(name, value);
                    }
                    await con.ExecuteAsync("usp_InsertItemOrder",param,commandType:CommandType.StoredProcedure);
                    res.message = param.Get<int>("retval") == 100 ? "Successful" : "Unsuccessful";
                    res.Result = res.message == "Successful" ? 100 : 101;
                }
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
            var conn = ConnectionString();
            try
            {
                using (var con = new SqlConnection(conn))
                {
                    con.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("retval", SqlDbType.Int ,direction: ParameterDirection.Output);
                    param.Add("@from",startDate,DbType.DateTime);
                    param.Add("@to", endDate, DbType.DateTime);
                    var ss = con.Query<T>("usp_InquiryDate", param, commandType: CommandType.StoredProcedure).AsList();
                    var res = await Task.Run(()=> _mapper.Map<List<OrderList1>>(ss));
                    var serial = JsonConvert.SerializeObject(res);
                    response.message = param.Get<int>("retval") == 100 ? "Successful" : "No Data Found!";
                    response.Result = res;
                    return response;
                }
            }
            catch (Exception ee)
            {
                response.message = ee.Message;
            }
            return response;
        }
        public async Task<Response<int>> RemoveRecord(int id)
        {
            var respnse = new Response<int>();
            var conn = ConnectionString();
            try
            {
                using (var con = new SqlConnection(conn))
                {
                    con.Open();
                    DynamicParameters param_ = new DynamicParameters();
                    param_.Add("retval", DbType.Int32,direction:ParameterDirection.Output);
                    param_.Add("id", id);
                    await con.ExecuteAsync("usp_removeItem ", param_,commandType:CommandType.StoredProcedure);
                    respnse.message = param_.Get<int>("retval") == 1 ? "Successful"  : "Unsuccessful";
                    respnse.Result = respnse.message == "Successful" ? 1 : 0;
                }
            }
            catch (Exception ee)
            {
                respnse.Result = 2;
                respnse.message = ee.Message;
            }
            return respnse;
        }

        public async Task<Response<int>> UpdateData(int id, Update listupd)
        {
            var response = new Response<int>();
            var con = ConnectionString();
            try
            {
                using (var sql = new SqlConnection(con))
                {
                    sql.Open();
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
                    await sql.ExecuteAsync("usp_UpdateItem", param: dynamic_, commandType: CommandType.StoredProcedure);
                    int res = dynamic_.Get<int>("retval");
                    response.message = res == 1 ? "Successful" : "UnSuccessful";
                    response.Result = response.message == "Successful" ? 1 : 0;
                }
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

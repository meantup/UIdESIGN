using API_Details.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Repository
{
    public interface IRepository
    {
        Task<Response<List<OrderList1>>> postItem(string startDate, string endDate);
        Task<Response<dynamic>> RemoveRecord(int icode);
        Task<Response<dynamic>> UpdateData(int icode, Update listUp);
        Task<Response<dynamic>> DataADD(Add add);
        Task<List<OrderList1>> selectAll();
    }
}

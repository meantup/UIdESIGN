using API_Details.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Repository
{
    public interface IRepository
    {
        Task<List<OrderList1>> postItem(string startDate, string endDate);
        Task<Response<int>> RemoveRecord(int icode);
        Task<Response<int>> UpdateData(int icode, Update listUp);
        Task<Response<int>> DataADD(Update add);
    }
}

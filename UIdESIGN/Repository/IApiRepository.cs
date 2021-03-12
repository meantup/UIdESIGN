using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIdESIGN.Models;

namespace UIdESIGN.Repository
{
    public interface IApiRepository
    {
        string Details(Inquiry inQu);
        string RemoveItem(int id);
        string PostData();
        string UpdateData(int id, Update update);
        string addProduct(Add add);
    }
}

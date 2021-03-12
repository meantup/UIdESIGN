using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIdESIGN.Repository;

namespace UIdESIGN.Class
{
    public class Adapter : IAdapterRepository
    {

       public IApiRepository api { get; }

        public Adapter()
        {
            api = new APIclass(baseUrl.defaultHost());
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

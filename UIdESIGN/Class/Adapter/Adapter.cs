using Microsoft.Extensions.Configuration;
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

        public Adapter(IConfiguration configuration)
        {
            api = new APIclass(configuration);
        }
    }
}

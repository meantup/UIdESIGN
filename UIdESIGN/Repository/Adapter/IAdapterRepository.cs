using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIdESIGN.Repository
{
    public interface IAdapterRepository
    {
        public IApiRepository api { get; }
    }
}

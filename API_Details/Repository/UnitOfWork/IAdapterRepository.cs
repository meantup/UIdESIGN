using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Repository
{
    public interface IAdapterRepository
    {
        IRepository repos { get; }
        IAccount account { get; }
    }
}

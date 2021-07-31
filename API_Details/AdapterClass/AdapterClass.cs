using API_Details.Model;
using API_Details.Repository;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API_Details.AdapterClass
{
    public class AdapterClass : IAdapterRepository
    {
        public IRepository repos { get; }
        public IAccount account { get; }

        public AdapterClass(IMapper mapper, IConfiguration config)
        {
            repos = new ModelBase(mapper, config);
            account = new AccountClass(config);
        }
    }
}

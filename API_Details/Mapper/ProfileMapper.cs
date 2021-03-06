using API_Details.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Details.Mapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<OrderList, OrderList1>().ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.iname))
                .ForMember(dest => dest.ProductDesc, opt => opt.MapFrom(src => src.idesc))
                .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.icode))
                .ForMember(dest => dest.photoImage,  opt => opt.MapFrom(src => src.image));
        }
    }
}

using System;
using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Domain.Entity;

namespace Pacagroup.Ecommerce.Transversal.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Customers, CustomersDTO>().ReverseMap();
            CreateMap<Users, UsersDTO>().ReverseMap();
            //CreateMap<Customers, CustomersDTO>().ReverseMap().ForMember(x => x.CustomerID, y => y.MapFrom(src => src.CustomerID));
        }
    }
}

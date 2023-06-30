using AutoMapper;
using Market.DAL.Entities.Market;
using Market.Domain.DTOs;
using System.ComponentModel;

namespace Market.BLL.Helpers
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Shop, ShopDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}

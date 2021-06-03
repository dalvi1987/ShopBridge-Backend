using AutoMapper;
using ShopBridge.Core;
using ShopBridge.Core.DTO;

namespace ShopBridge.Infra.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Unit, UnitDTO>().ReverseMap();
            
        }
    }
}

using AutoMapper;
using Talabat.Apis.Dtos;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities.Product;
using Talabat.Domain.Entities.Basket;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
        }
    }
}

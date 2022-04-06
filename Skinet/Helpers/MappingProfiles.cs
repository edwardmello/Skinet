using AutoMapper;
using Core.Models;
using Skinet.Dtos;

namespace Skinet.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.productBrand, o => o.MapFrom(s => s.productBrand.Name))
                .ForMember(d => d.productType, o => o.MapFrom(s => s.productType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

        }
    }
}

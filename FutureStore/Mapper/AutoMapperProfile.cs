using AutoMapper;
using FutureStore.DTOs.Category;
using FutureStore.Models;

namespace FutureStore.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<ProductGet, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        
        
        }
    }
}

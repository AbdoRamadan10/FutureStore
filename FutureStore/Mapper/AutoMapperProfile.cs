using AutoMapper;
using FutureStore.DTOs.Category;
using FutureStore.Models.Buisness;

namespace FutureStore.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<ProductGet, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<ProductPost, Product>()
               .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId)).ReverseMap();

            CreateMap<CategoryGet, Category>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<CategoryPost, Category>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();



        }
    }
}

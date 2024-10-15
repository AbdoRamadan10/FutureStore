using AutoMapper;
using FutureStore.DTOs.Category;
using FutureStore.Models;

namespace FutureStore.Services
{
    public class ProductService
    {
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper) 
        {
            _mapper = mapper;
        }

        public ProductGet ConvertToProductGet(Product product)
        { 
            ProductGet productGet = _mapper.Map<ProductGet>(product);
            return productGet;
        }
    }
}

using FutureStore.DTOs.BaseDTO;
using FutureStore.Models;

namespace FutureStore.DTOs.Category
{
    public class CategoryGet : BaseGet
    {
        public IEnumerable<ProductGet> Products { get; set; } 
    }
}

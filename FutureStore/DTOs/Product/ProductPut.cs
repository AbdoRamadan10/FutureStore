using FutureStore.DTOs.BaseDTO;

namespace FutureStore.DTOs.Category
{
    public class ProductPut : BasePut
    {
        public string? Code { get; set; } 
        public double? Price { get; set; }

        public int? CategoryId { get; set; }
    }
}

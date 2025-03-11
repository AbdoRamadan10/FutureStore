using FutureStore.DTOs.BaseDTO;

namespace FutureStore.DTOs.Category
{
    public class ProductGet : BaseGet
    {
        public string? Code { get; set; }
        public double? Price { get; set; }

        public double? Quantity { get; set; }

        public int? CategoryId { get; set; }

        public string? CategoryNameAR { get; set; }
        public string? CategoryNameEN { get; set; }


    }
}

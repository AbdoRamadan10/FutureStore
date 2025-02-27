namespace FutureStore.Models.Buisness
{
    public class Product : BaseModel
    { 
        public string? Code { get; set; }
        public double? Price { get; set; } = 0;
        public int? CategoryId { get; set; }

        public Category? Category { get; set; }



    }
}

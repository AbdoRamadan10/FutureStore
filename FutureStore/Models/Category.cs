namespace FutureStore.Models
{
    public class Category : BaseModel
    {
        public IEnumerable<Product>? Products { get; set; } 
    }
}

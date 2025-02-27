namespace FutureStore.Models.Buisness
{
    public class Category : BaseModel
    {
        public IEnumerable<Product>? Products { get; set; } 
    }
}

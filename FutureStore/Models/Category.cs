namespace FutureStore.Models
{
    public class Category
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>(); 
    }
}

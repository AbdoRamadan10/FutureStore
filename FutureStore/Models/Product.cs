namespace FutureStore.Models
{
    public class Product : BaseModel
    { 
        public string Code { get; set; }
        public double Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }



    }
}

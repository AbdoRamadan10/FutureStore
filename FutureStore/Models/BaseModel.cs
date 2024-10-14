namespace FutureStore.Models
{
    public class BaseModel
    {
        public int Id { get; set; }

        public string NameAR { get; set; } =string.Empty;

        public string NameEN { get; set; } = string.Empty;

        public string Description { get; set; } =string.Empty;

        public DateTime CreatedTimeStamp { get; set; } = DateTime.Now;


    }
}

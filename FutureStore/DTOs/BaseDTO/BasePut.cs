namespace FutureStore.DTOs.BaseDTO
{
    public class BasePut 
    {
        public string? NameAR { get; set; } 

        public string? NameEN { get; set; } 

        public string? Description { get; set; } 

        //public DateTime? UpdatedTimeStamp { get; set; } = DateTime.Now;

        public bool? Active { get; set; } 
    }
}

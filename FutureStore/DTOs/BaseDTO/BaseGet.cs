﻿namespace FutureStore.DTOs.BaseDTO
{
    public class BaseGet
    {
        public int Id { get; set; }

        public string NameAR { get; set; }

        public string NameEN { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedTimeStamp { get; set; }

        public DateTime? UpdatedTimeStamp { get; set; }

        public bool Active { get; set; } 


    }
}

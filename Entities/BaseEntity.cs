﻿namespace WebApplication4.Entities
{
	public class BaseEntity
	{
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

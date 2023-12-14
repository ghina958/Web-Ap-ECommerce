﻿using System.ComponentModel.DataAnnotations.Schema;

namespace JobTaskProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? Image { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}

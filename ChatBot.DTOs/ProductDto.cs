using System;
using System.Configuration;

namespace ChatBot.DTOs
{
    [Serializable]
    public class ProductDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string SKU { get; set; }

        public int? CategoryId { get; set; }

        public string ImageUrl { get; set; }
    }
}

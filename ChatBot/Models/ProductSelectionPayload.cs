using ChatBot.DTOs;

namespace LuisBot.Models
{
    public class ProductSelectionPayload
    {
        public string Action { get; set; }

        public ProductDto Product { get; set; }
    }
}
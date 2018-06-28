using System;

namespace ChatBot.DTOs
{
    public class ShoppingItemDto
    {
        public Guid Id { get; set; }

        public int? ProductId { get; set; }

        public int? CategoryId { get; set; }

        public string Description { get; set; }

        public int SortIndex { get; set; }

        public string Label { get; set; }

        public Pin Color { get; set; }

        public int? X { get; set; }

        public int? Y { get; set; }
    }
}

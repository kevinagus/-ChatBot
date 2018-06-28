using System;
using System.Collections.Generic;

namespace ChatBot.DTOs
{
    [Serializable]
    public class ShoppingListDto
    {
        public Guid Id { get; set; }

        public IList<ShoppingItemDto> Items { get; set; }
    }
}

using ChatBot.DTOs.RecommendationEngine;
using System.Collections.Generic;

namespace ChatBot.DTOs
{
    public class RecommendationDto
    {
        public MatcherResponse Matcher { get; set; }

        public IList<ProductDto> Products { get; set; }
    }
}

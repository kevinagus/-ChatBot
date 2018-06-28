using System.Collections.Generic;

namespace ChatBot.DTOs.RecommendationEngine
{
    public class MatcherResponse
    {
        public IList<MatcherItem> Results { get; set; }
    }
}

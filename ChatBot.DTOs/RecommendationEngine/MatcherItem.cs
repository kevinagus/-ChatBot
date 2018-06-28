namespace ChatBot.DTOs.RecommendationEngine
{
    public class MatcherItem
    {
        public string Raw { get; set; }

        public string Type { get; set; }

        public int Id { get; set; }

        public double Score { get; set; }
    }
}

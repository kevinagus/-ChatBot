using Newtonsoft.Json;

namespace LuisBot.Models.FacebookModels
{
    public class FacebookAdjustment
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public int Amount { get; set; }
    }
}
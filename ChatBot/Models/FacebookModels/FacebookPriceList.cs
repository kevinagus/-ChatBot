using Newtonsoft.Json;

namespace LuisBot.Models.FacebookModels
{
    public class FacebookPriceList
    {
        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public string Amount { get; set; }
    }
}
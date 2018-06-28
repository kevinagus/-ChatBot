using Newtonsoft.Json;

namespace LuisBot.Models.FacebookModels
{
    public class FacebookSummary
    {
        [JsonProperty("subtotal", NullValueHandling = NullValueHandling.Ignore)]
        public float Subtotal { get; set; }
        [JsonProperty("shipping_cost", NullValueHandling = NullValueHandling.Ignore)]
        public float Shipping_cost { get; set; }
        [JsonProperty("total_tax", NullValueHandling = NullValueHandling.Ignore)]
        public float Total_tax { get; set; }
        [JsonProperty("total_cost", NullValueHandling = NullValueHandling.Ignore)]
        public float Total_cost { get; set; }
    }
}
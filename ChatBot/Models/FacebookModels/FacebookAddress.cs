using Newtonsoft.Json;

namespace LuisBot.Models.FacebookModels
{
    public class FacebookAddress
    {
        [JsonProperty("street_1", NullValueHandling = NullValueHandling.Ignore)]
        public string Street_1 { get; set; }
        [JsonProperty("street_2", NullValueHandling = NullValueHandling.Ignore)]
        public string Street_2 { get; set; }
        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }
        [JsonProperty("postal_code", NullValueHandling = NullValueHandling.Ignore)]
        public string Postal_code { get; set; }
        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }
    }
}
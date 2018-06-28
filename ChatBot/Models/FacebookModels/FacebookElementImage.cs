using LuisBot.Interfaces;
using Newtonsoft.Json;

namespace LuisBot.Models.FacebookModels
{
    public class FacebookElementImage : IFacebookElement
    {
        [JsonProperty("media_type", NullValueHandling = NullValueHandling.Ignore)]
        public string Media_type { get; set; }
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        [JsonProperty("is_reusable", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsReusable { get; set; }
    }
}
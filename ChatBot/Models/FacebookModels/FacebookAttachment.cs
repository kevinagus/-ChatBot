using Newtonsoft.Json;

namespace LuisBot.Models.FacebookModels
{
    public class FacebookAttachment
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Payload { get; set; }
    }
}
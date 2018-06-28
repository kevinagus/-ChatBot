using Newtonsoft.Json;

namespace LuisBot.Models.FacebookModels
{
    public class FacebookQuickReply
    {
        [JsonProperty("content_type", NullValueHandling = NullValueHandling.Ignore)]
        public string Content_type { get; set; }
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public string Payload { get; set; }
    }
}
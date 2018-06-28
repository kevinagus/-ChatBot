using LuisBot.Interfaces;
using Newtonsoft.Json;

namespace LuisBot.Models.FacebookModels
{
    public class FacebookMessage : IFacebookMessage
    {
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        [JsonProperty("notification_type", NullValueHandling = NullValueHandling.Ignore)]
        public string Notification_type { get; set; }
        [JsonProperty("attachment", NullValueHandling = NullValueHandling.Ignore)]
        public FacebookAttachment Attachment { get; set; }
    }
}
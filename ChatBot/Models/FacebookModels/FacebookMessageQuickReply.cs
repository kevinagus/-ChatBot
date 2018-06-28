using LuisBot.Interfaces;
using Newtonsoft.Json;

namespace LuisBot.Models.FacebookModels
{
    public class FacebookMessageQuickReply : IFacebookMessage
    {
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        [JsonProperty("quick_replies", NullValueHandling = NullValueHandling.Ignore)]
        public FacebookQuickReply[] Quick_replies { get; set; }
    }
}
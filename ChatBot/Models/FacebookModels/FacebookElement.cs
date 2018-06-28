using LuisBot.Interfaces;
using Newtonsoft.Json;

namespace LuisBot.Models.FacebookModels
{
    public class FacebookElement : IFacebookElement
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        [JsonProperty("subtitle", NullValueHandling = NullValueHandling.Ignore)]
        public string Subtitle { get; set; }
        [JsonProperty("item_url", NullValueHandling = NullValueHandling.Ignore)]
        public string Item_url { get; set; }
        [JsonProperty("default_action", NullValueHandling = NullValueHandling.Ignore)]
        public FacebookButton Default_action { get; set; }
        [JsonProperty("image_url", NullValueHandling = NullValueHandling.Ignore)]
        public string Image_url { get; set; }
        [JsonProperty("buttons", NullValueHandling = NullValueHandling.Ignore)]
        public FacebookButton[] Buttons { get; set; }
        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

    }
}
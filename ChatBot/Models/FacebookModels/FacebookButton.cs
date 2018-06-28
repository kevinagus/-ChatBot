using Newtonsoft.Json;

namespace LuisBot.Models.FacebookModels
{
    public class FacebookButton
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
        public string Payload { get; set; }
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        [JsonProperty("messenger_extensions", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MessengerExtensions { get; set; }
        [JsonProperty("webview_height_ratio", NullValueHandling = NullValueHandling.Ignore)]
        public string Webview_height_ratio { get; set; }
        [JsonProperty("fallback_url", NullValueHandling = NullValueHandling.Ignore)]
        public string Fallback_url { get; set; }
        [JsonProperty("payment_summary", NullValueHandling = NullValueHandling.Ignore)]
        public FacebookPaymentSummary Payment_summary { get; set; }
    }
}
using LuisBot.Interfaces;
using Newtonsoft.Json;

namespace LuisBot.Models.FacebookModels
{
    public class FacebookPayload
    {
        [JsonProperty("template_type", NullValueHandling = NullValueHandling.Ignore)]
        public string Template_type { get; set; }
        [JsonProperty("top_element_style", NullValueHandling = NullValueHandling.Ignore)]
        public string TopElementStyle { get; set; }
        [JsonProperty("recipient_name", NullValueHandling = NullValueHandling.Ignore)]
        public string Recipient_name { get; set; }
        [JsonProperty("order_number", NullValueHandling = NullValueHandling.Ignore)]
        public string Order_number { get; set; }
        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }
        [JsonProperty("payment_method", NullValueHandling = NullValueHandling.Ignore)]
        public string Payment_method { get; set; }
        [JsonProperty("order_url", NullValueHandling = NullValueHandling.Ignore)]
        public string Order_url { get; set; }
        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public string Timestamp { get; set; }
        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public FacebookAddress Address { get; set; }
        [JsonProperty("summary", NullValueHandling = NullValueHandling.Ignore)]
        public FacebookSummary Summary { get; set; }
        [JsonProperty("adjustments", NullValueHandling = NullValueHandling.Ignore)]
        public FacebookAdjustment[] Adjustments { get; set; }
        [JsonProperty("elements", NullValueHandling = NullValueHandling.Ignore)]
        public IFacebookElement[] Elements { get; set; }
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        [JsonProperty("buttons", NullValueHandling = NullValueHandling.Ignore)]
        public FacebookButton[] Buttons { get; set; }
        [JsonProperty("merchant_name", NullValueHandling = NullValueHandling.Ignore)]
        public string Merchant_name { get; set; }
    }
}
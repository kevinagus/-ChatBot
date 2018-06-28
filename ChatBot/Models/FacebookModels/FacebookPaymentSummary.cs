using Newtonsoft.Json;

namespace LuisBot.Models.FacebookModels
{
    public class FacebookPaymentSummary
    {
        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }
        [JsonProperty("is_test_payment", NullValueHandling = NullValueHandling.Ignore)]
        public bool Is_test_payment { get; set; }
        [JsonProperty("payment_type", NullValueHandling = NullValueHandling.Ignore)]
        public string Payment_type { get; set; }
        [JsonProperty("merchant_name", NullValueHandling = NullValueHandling.Ignore)]
        public string Merchant_name { get; set; }
        [JsonProperty("requested_user_in_fo", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Requested_user_in_fo { get; set; }
        [JsonProperty("price_list", NullValueHandling = NullValueHandling.Ignore)]
        public FacebookPriceList[] Price_list { get; set; }
    }
}
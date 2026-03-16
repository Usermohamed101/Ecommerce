using System.Text.Json.Serialization;

namespace Ecommerce.Dtos.Fawaterak.CallBacks
{
    public class RefundWebHook
    {
        [JsonPropertyName("transactionId")]
        public string transactionId { get; set; }
        [JsonPropertyName("amount")]
        public string amount { get; set; }
        [JsonPropertyName("currency")]
        public string currency { get; set; }
        [JsonPropertyName("transactionId")]
        public string status { get; set; }
        [JsonPropertyName("reason")]
        public string reason { get; set; }
        [JsonPropertyName("approvedAt")]
        public string approvedAt { get; set; }


        [JsonPropertyName("payLoad")]
        public payLoad payload { get; set; }



    }
}


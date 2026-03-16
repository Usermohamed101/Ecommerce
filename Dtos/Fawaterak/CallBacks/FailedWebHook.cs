using System.Text.Json.Serialization;

namespace Ecommerce.Dtos.Fawaterak.CallBacks
{
    public class FailedWebHook
    {
        [JsonPropertyName("invoice_key")]
        public string invoiceKey { get; set; }
        [JsonPropertyName("invoice_id")]
        public long invoiceId { get; set; }
        [JsonPropertyName("payment_method")]
        public string paymentMethod { get; set; }
        [JsonPropertyName("pay_load")]
        public string payLoad { get; set; }
        [JsonPropertyName("amount")]
        public long amount { get; set; }
        [JsonPropertyName("paidCurrency")]
        public string paidCurrency { get; set; }
        [JsonPropertyName("errorMessage")]
        public string errorMessage { get; set; }
        [JsonPropertyName("response")]
        public FailedWebHookResponse response { get; set; }
        [JsonPropertyName("referenceNumber")]
        public string refrenceNumber { get; set; }

       
    }
    public class FailedWebHookResponse
    {
        [JsonPropertyName("gatewayCode")]
        public string gatewayCode { get; set; }
        [JsonPropertyName("gatewayRecommendation")]
        public string gatewayRecomendation { get; set; }
    }

    
}






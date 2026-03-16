using System.Text.Json.Serialization;

namespace Ecommerce.Dtos.Fawaterak
{
    public class FawreyResponse:PaymentResponse
    {

        [JsonPropertyName("data")]
        public FaweryData Data { get; set; }
    }

    public class FaweryData
    {
        [JsonPropertyName("invoice_id")]
        public long InvoiceId { get; set; }
        [JsonPropertyName("invoice_key")]
        public string InoviceKey { get; set; }

        [JsonPropertyName("payment_data")]
        public FaweryPaymentData PaymentDate { get; set; }
        [JsonPropertyName("payLoad")]
        public payLoad payload { get; set; }

    }

    public class FaweryPaymentData
    {
        [JsonPropertyName("fawryCode")]
        public string FaweryCode { get; set; }
        [JsonPropertyName("expireDate")]
        public string expireDate { get; set; }

    }


}



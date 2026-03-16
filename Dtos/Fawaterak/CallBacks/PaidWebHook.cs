using System.Text.Json.Serialization;

namespace Ecommerce.Dtos.Fawaterak.CallBacks
{
    public class PaidWebHook
    {
        [JsonPropertyName("hashKey")]
        public string hashKey { get; set; }
        [JsonPropertyName("invoice_key")]
        public string invoiceKey { get; set; }
        [JsonPropertyName("invoice_id")]
        public long invoiceId { get; set; }
        [JsonPropertyName("payment_method")]
        public string paymentMethod { get; set; }
        [JsonPropertyName("invoice_status")]
        public string invoiceStatus { get; set; }
        [JsonPropertyName("referenceNumber")]
        public string refrenceNumber { get; set; }

        [JsonPropertyName("pay_load")]

        public string payLoad { get; set; }
        public payLoad? payloadObject { get; set; }


    }
}












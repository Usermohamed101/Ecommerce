using System.Text.Json.Serialization;

namespace Ecommerce.Dtos.Fawaterak
{
    public class MasterCardResponse : PaymentResponse
    {
        [JsonPropertyName("data")]
        public MasterCardData Data { get; set; }


    }

    public class MasterCardData
    {
        [JsonPropertyName("invoice_id")]
        public long InvoiceId { get; set; }
        [JsonPropertyName("invoice_key")]
        public string InvoiceKey { get; set; }
        [JsonPropertyName("payment_data")]
        public MasterCardPaymentData paymentData { get; set; }
        [JsonPropertyName("payLoad")]
        public payLoad payload { get; set; }
    }
    public class MasterCardPaymentData
    {
        [JsonPropertyName("redirectTo")]
        public string RedirectTo { get; set; }
    }
}




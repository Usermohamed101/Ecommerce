using System.Text.Json.Serialization;

namespace Ecommerce.Dtos.Fawaterak
{
    public class MobileWalletResponse:PaymentResponse
    {
        [JsonPropertyName("data")]
        public MobileWalletData Data { get; set; }
    }

    public class MobileWalletData
    {
        [JsonPropertyName("invoice_id")]
        public long InvoiceId { get; set; }
        [JsonPropertyName("invoice_key")]
        public string InoviceKey { get; set; }

        [JsonPropertyName("payment_data")]
        public MobileWalletPaymentData PaymentDate { get; set; }

        [JsonPropertyName("payLoad")]
        public payLoad payload { get; set; }
    }

    public class MobileWalletPaymentData
    {
        [JsonPropertyName("meezaReference")]
        public long Refrence { get; set; }
        [JsonPropertyName("meezaQrCode")]
        public string QrCode { get; set; }

    }


}


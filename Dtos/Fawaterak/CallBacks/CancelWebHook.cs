using System.Text.Json.Serialization;

namespace Ecommerce.Dtos.Fawaterak.CallBacks
{
    public class CancelWebHook
    {

        [JsonPropertyName("hashKey")]
        public string hashKey { get; set; }
        [JsonPropertyName("referenceId")]
        public string refrenceId { get; set; }
        [JsonPropertyName("status")]
        public string status { get; set; }

        [JsonPropertyName("paymentMethod")]
        public string paymentMethod { get; set; }

        //[JsonPropertyName("pay_load")]
        //public string payLoad { get; set; }

        [JsonPropertyName("transactionId")]
        public long transactionId { get; set; }
       
        [JsonPropertyName("transactionKey")]
        public string transactionKey { get; set; }
        [JsonPropertyName("payLoad")]
        public payLoad payload { get; set; }
    }
}

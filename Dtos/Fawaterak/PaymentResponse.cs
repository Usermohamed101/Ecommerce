using System.Text.Json.Serialization;

namespace Ecommerce.Dtos.Fawaterak
{
    public class PaymentResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } 
    }
}


using System.Text.Json.Serialization;

namespace Ecommerce.Dtos.Fawaterak
{
    public class PaymentMethod
    {
        [JsonPropertyName("paymentId")]
        public int Id { get; set; }
        [JsonPropertyName("name_en")]
        public string Name_En{ get; set; }
        [JsonPropertyName("name_ar")]
        public string Name_Ar { get; set; }
        [JsonPropertyName("redirect")]
        public string Redirect { get; set; }
        [JsonPropertyName("logo")]
        public string Logo { get; set; }
    }
      
}

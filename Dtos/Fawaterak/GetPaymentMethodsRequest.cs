using System.Text.Json.Serialization;

namespace Ecommerce.Dtos.Fawaterak
{
    public class GetPaymentMethodsRequest
    {
        [JsonPropertyName("status")]
        public string status { get; set; }
        [JsonPropertyName("vendorSettingsData")]
      
        public VendorSettingsData settings { get; set; }

        [JsonPropertyName("data")]
        public List<PaymentMethod> PaymentMethods { get; set; }

    }



    public class VendorSettingsData()
    {
        [JsonPropertyName("custome_iframe_title")]
        public string Custome_iframe_title { get; set; }
    };
  
}

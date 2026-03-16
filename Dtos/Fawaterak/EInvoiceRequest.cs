using System.Globalization;
using System.Text.Json.Serialization;

namespace Ecommerce.Dtos.Fawaterak
{
    public class EInvoiceRequest
    {
        [JsonPropertyName("payment_method_id")]
        public int PaymentMethodId { get; set; }
        [JsonPropertyName("cartTotal")]
        public decimal CartTotal { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }
        [JsonPropertyName("redirectionUrls")]
        public RedirectionUrls Urls { get; set; }
        [JsonPropertyName("cartItems")]
        public List<CartItem> CartItems { get; set; }

        [JsonPropertyName("payLoad")]
        public  payLoad PayLoad{get;set;}
    }
    public class Customer()
    {
        [JsonPropertyName("first_name")]
        public string FName { get; set; }
        [JsonPropertyName("last_name")]
        public string LName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("Address")]
        public string Address { get; set; }

    }

    public class RedirectionUrls()
    {
        [JsonPropertyName("successUrl")]
        public string SuccessfulUrl { get; set; }
        [JsonPropertyName("failUrl")]
        public string FailUrl { get; set; }
        [JsonPropertyName("pendingUrl")]
        public string PendingUrl { get; set; }
    }

    public class CartItem()
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }


    }


    public class payLoad
    {
        [JsonPropertyName("orderId")]
      public  int orderId { get; set; }
    }

}





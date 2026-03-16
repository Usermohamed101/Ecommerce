using Ecommerce.infrastruction;
using Ecommerce.Models;

namespace Ecommerce.Dtos.Order
{
    public class OrderBase
    {

       
        

        public decimal TotalPrice { get; set; }
        public int ShippingAddressId { get; set; }
       public OrderStatus Status { get; set; }

        public int DeliveryOptionId { get; set; }
       

       

    }
}

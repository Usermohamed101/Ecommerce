using Ecommerce.Dtos.Order;
using Ecommerce.Dtos.Product;

namespace Ecommerce.Dtos.OrderItem
{
    public class GetOrderItem:OrderBase
    {

        public int Id { get; set; }
     
        public GetOrder Order { get; set; }

        public int ProductId { get; set; }
        public GetProduct Product { get; set; }

    
    }
}

using Ecommerce.Dtos.OrderItem;
using Ecommerce.Dtos.PaymentDetailsDto;

namespace Ecommerce.Dtos.Order
{
    public class GetOrder:OrderBase
    {
    
        public int Id { get; set; }

        public ICollection<GetOrderItem> Items { get; set; } = new List<GetOrderItem>();

        public ICollection<GetPaymentDetails> PaymentDetails { get; set; } = new List<GetPaymentDetails>();
    }
}

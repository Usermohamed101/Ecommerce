using Ecommerce.Dtos.Order;

namespace Ecommerce.Dtos.PaymentDetailsDto
{
    public class GetPaymentDetails:PaymentDetailsBase
    {

        public int Id { get; set; }

        public GetOrder order { get; set; }
    }
}

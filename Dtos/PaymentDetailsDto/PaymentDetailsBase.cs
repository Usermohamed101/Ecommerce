using Ecommerce.Models;

namespace Ecommerce.Dtos.PaymentDetailsDto
{
    public class PaymentDetailsBase
    {

       

        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }

        public PaymentMethods PaymentMethod { get; set; }

     


    }
}

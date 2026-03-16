using Ecommerce.Models;

namespace Ecommerce.infrastruction
{
    public class PaymentDetails
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }

        public PaymentMethods PaymentMethod { get; set; }

        public DateTime CreatedAt  => DateTime.UtcNow;
        public virtual Order Order { get; set; }


        public PaymentDetails()
        {
            
        }
        public  PaymentDetails(int orderId,decimal amount,PaymentMethods paymentMethod)
        {
            if (amount < 0) { throw new AggregateException("amount must be positive."); }

            OrderId = orderId;
            Amount = amount;
            PaymentMethod = paymentMethod;
            Status = PaymentStatus.Pending;
        }

        public void MarkAsPaid()
        {

            if (Status != PaymentStatus.Pending)
            {
                throw new InvalidOperationException("only pending payments can be Paid");
            }
            Status = PaymentStatus.Paid;
        } 

        public void MarkAsCancelled()
        {

            if (Status != PaymentStatus.Pending)
            {
                throw new InvalidOperationException("only pending payments can be Cancelled");
            }
            Status = PaymentStatus.Cancelled;
        }

        public void MarkAsFailed()
        {
            if (Status != PaymentStatus.Pending)
            {
                throw new InvalidOperationException("only pending payments can be Failed");
            }
            Status = PaymentStatus.Failed;

        }

        public void MarkAsRefunded()
        {
            if (Status != PaymentStatus.Paid)
            {
                throw new InvalidOperationException("only Paid payments can be Refunded");
            }
            Status = PaymentStatus.Refunded;
        }

    }
}

using Ecommerce.infrastruction;

namespace Ecommerce.Repository
{
    public interface IPaymentDetailsRepo:IGeneric<PaymentDetails,int>
    {




        Task<ICollection<PaymentDetails>> GetPaymentDetailsByOrderId(int id);

   

    }
}

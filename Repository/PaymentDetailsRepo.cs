using Ecommerce.infrastruction;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class PaymentDetailsRepo(ECommerceContext contxt) : GenericRepo<PaymentDetails, int>(contxt), IPaymentDetailsRepo
    {
        public async Task<ICollection<PaymentDetails>> GetPaymentDetailsByOrderId(int id)
        {
            return await contxt.Set<PaymentDetails>().Where(pd => pd.OrderId == id).ToListAsync();
        }
    }
}

using Ecommerce.infrastruction;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class CartRepo(ECommerceContext contxt) : GenericRepo<Cart, int>(contxt), ICartRepo
    {
        public async Task<Cart> GetByUserIdAsync(string userId)
        {
            return await contxt.Set<Cart>().Include(c=>c.Items).ThenInclude(i=>i.Product).FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}

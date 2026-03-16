using Ecommerce.infrastruction;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class WishListRepo(ECommerceContext contxt) : GenericRepo<WishList, int>(contxt), IWishListRepo
    {
        public Task<WishList> GetByUserIdAsync(string userId)
        {
            return contxt.Set<WishList>().FirstOrDefaultAsync(w => w.UserId == userId);
        }
    }
}

using Ecommerce.infrastruction;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class ReviewRepo(ECommerceContext contxt) : GenericRepo<Review, int>(contxt), IReviewRepo
    {
        public async Task<bool> ExistsAsync(string userId, int productId)
        {
          return await contxt.Set<Order>().Include(o=> o.Items).Where( o =>o.Status==OrderStatus.Delivered&& o.UserId == userId && o.Items.Any(i=>i.Id==productId)).AnyAsync();
         
        }

        public async Task<List<Review>> GetByProductId(int id)
        {

            return await contxt.Set<Review>().Where(r => r.ProductId==id).ToListAsync();

        }
    }
}

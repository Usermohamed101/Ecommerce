using Ecommerce.infrastruction;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class CategoryRepo(ECommerceContext contxt) : GenericRepo<Category, int>(contxt), ICategoryRepo
    {
        public async Task<List<Product>> GetProductsByCategoryIdAsync(int id)
        {
            return await contxt.Set<Product>().Where(c => c.CategoryId == id).ToListAsync();
        }
    }
}

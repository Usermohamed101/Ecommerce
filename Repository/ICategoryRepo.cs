using Ecommerce.infrastruction;

namespace Ecommerce.Repository
{
    public interface ICategoryRepo:IGeneric<Category,int>
    {

      

        Task<List<Product>> GetProductsByCategoryIdAsync(int id);
        


    }
}

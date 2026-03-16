using Ecommerce.infrastruction;

namespace Ecommerce.Repository
{
    public interface ICartRepo:IGeneric<Cart,int>
    {


        Task<Cart> GetByUserIdAsync(string userId);



    }
}

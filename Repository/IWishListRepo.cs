using Ecommerce.infrastruction;

namespace Ecommerce.Repository
{
    public interface IWishListRepo:IGeneric<WishList,int>
    {
  

        Task<WishList> GetByUserIdAsync(string userId);
       


    }
}

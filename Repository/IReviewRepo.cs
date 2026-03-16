using Ecommerce.Dtos.Review;
using Ecommerce.infrastruction;

namespace Ecommerce.Repository
{
    public interface IReviewRepo:IGeneric<Review,int>
    {


        Task<List<Review>> GetByProductId(int id);


        Task<bool> ExistsAsync(string userId, int productId);

      


    }
}

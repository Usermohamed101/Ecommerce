using AutoMapper;
using Ecommerce.Dtos.Product;
using Ecommerce.Dtos.Review;
using Ecommerce.Dtos.Service;
using Ecommerce.infrastruction;
using Ecommerce.Repository;

namespace Ecommerce.Services
{
    public interface IReviewService
    {


        Task<ICollection<GetReview>> GetAllAsync();
        Task<GetReview> GetByIdAsync(int id);

        Task<ServiceResponse> Add(CreateReview e);

        Task<ServiceResponse> Delete(int id);

        Task<ServiceResponse> Update(UpdateReview e);

        Task<ICollection<GetReview>> GetReviewsByProductId(int id);
        Task<ICollection<GetReview>> GetReviewsByUserId(string id);

    }

    public class ReviewService(IMapper mapper, IReviewRepo repo) : IReviewService
    {
        public async Task<ServiceResponse> Add(CreateReview e)
        {
            var res = mapper.Map<Review>(e);
            repo.Add(res);
            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "Review is created")
            : new ServiceResponse(false, "Review couldnt be created");


        }

        public async Task<ServiceResponse> Delete(int id)
        {
            var res = await repo.GetByIdAsync(id);
            repo.Delete(res);
            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "Review is deleted")
             : new ServiceResponse(false, "Review couldnt be deleted");

        }

        public async Task<ICollection<GetReview>> GetAllAsync()
        {
            var res = await repo.GetAllAsync();
            return mapper.Map<ICollection<GetReview>>(res);
        }

        public async Task<GetReview> GetByIdAsync(int id)
        {
            var res = await repo.GetByIdAsync(id);
            return mapper.Map<GetReview>(res);
        }

        public async Task<ServiceResponse> Update(UpdateReview e)
        {

            var res = mapper.Map<Review>(e);
            repo.Update(res);
            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "Review is updated")
             : new ServiceResponse(false, "Review couldnt be updated");

        }

        public async Task<ICollection<GetReview>> GetReviewsByProductId(int id)
        {
            var reviews = (await repo.GetAllAsync()).Where(r => r.ProductId == id);
            return mapper.Map<List<GetReview>>(reviews);
        }

        public async Task<ICollection<GetReview>> GetReviewsByUserId(string id)
        {
            var reviews = (await repo.GetAllAsync()).Where(r => r.UserId == id);
            return mapper.Map<List<GetReview>>(reviews);
        }

    }
}


using AutoMapper;
using Ecommerce.Dtos.Cart;
using Ecommerce.Dtos.Category;
using Ecommerce.Dtos.Service;
using Ecommerce.infrastruction;
using Ecommerce.Repository;

namespace Ecommerce.Services
{
    public interface ICategoryService
    {

        Task<ICollection<GetCategory>> GetAllAsync();
        Task<GetCategory> GetByIdAsync(int id);

        Task<ServiceResponse> Add(CreateCategory e);

        Task<ServiceResponse> Delete(int id);

        Task<ServiceResponse> Update(UpdateCategory e);
    }





    public class CategoryService(ICategoryRepo repo, IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResponse> Add(CreateCategory e)
        {

            var res = mapper.Map<Category>(e);
            repo.Add(res);
            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "category is created")
                : new ServiceResponse(false, "category couldnt be created");



        }

        public async Task<ServiceResponse> Delete(int id)
        {
            var res =await repo.GetByIdAsync(id);


            repo.Delete(res);

            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "category is deleted")
               : new ServiceResponse(false, "category couldnt be deleted");
        }

        public async Task<ICollection<GetCategory>> GetAllAsync()
        {
            var res = await repo.GetAllAsync();
            return mapper.Map<ICollection<GetCategory>>(res);
        }

        public async Task<GetCategory> GetByIdAsync(int id)
        {
            var res = await repo.GetByIdAsync(id);

            return mapper.Map<GetCategory>(res);
        }

        public async Task<ServiceResponse> Update(UpdateCategory e)
        {
            var res = mapper.Map<Category>(e);
            repo.Update(res);

            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "category is updated")
               : new ServiceResponse(false, "category couldnt be updated");
        }
    }
}

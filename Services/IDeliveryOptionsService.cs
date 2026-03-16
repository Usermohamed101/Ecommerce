using AutoMapper;
using Ecommerce.Dtos.Category;
using Ecommerce.Dtos.DeliveryOption;
using Ecommerce.Dtos.Service;
using Ecommerce.infrastruction;
using Ecommerce.Repository;

namespace Ecommerce.Services
{
    public interface IDeliveryOptionsService
    {


    
        Task<GetDeliveryOption> GetByIdAsync(int id);

        Task<decimal> CalculateCostOfDelivery(int id);

        Task<ICollection<GetDeliveryOption>> GetAllAsync();
       

        Task<ServiceResponse> Add(CreateDeliveryOption e);

        Task<ServiceResponse> Delete(int id);

        Task<ServiceResponse> Update(UpdateDeliveryOption e);


        public class DeliveryOptionService(IDeliveryOptionRepo repo, IMapper mapper) : IDeliveryOptionsService
        {
            public async Task<ServiceResponse> Add(CreateDeliveryOption e)
            {
                var res = mapper.Map<DeliveryOption>(e);
                repo.Add(res);
                return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "DeliveryOption is created")
                    : new ServiceResponse(false, "DeliveryOption couldnt be created");


            }

            public async Task<decimal> CalculateCostOfDelivery(int id)
            {
                var res = await repo.GetByIdAsync(id);
                return res.DeliveryPrice;
            }

            public async Task<ServiceResponse> Delete(int id)
            {

                var res = await repo.GetByIdAsync(id);
                repo.Delete(res);

                return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "DeliveryOption is deledted")
                 : new ServiceResponse(false, "DeliveryOption couldnt be deledted");



            }

            public async Task<ICollection<GetDeliveryOption>> GetAllAsync()
            {
                var res = await repo.GetAllAsync();
                return mapper.Map<ICollection<GetDeliveryOption>>(res);
            }

            public async Task<GetDeliveryOption> GetByIdAsync(int id)
            {
                var res = await repo.GetByIdAsync(id);
                return mapper.Map<GetDeliveryOption>(res);
            }

            public async Task<ServiceResponse> Update(UpdateDeliveryOption e)
            {
                var res = mapper.Map<DeliveryOption>(e);
                repo.Update(res);
                return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "DeliveryOption is updated")
                 : new ServiceResponse(false, "DeliveryOption couldnt be updated");





            }
        }
    }
}

using AutoMapper;
using Ecommerce.Dtos.Image;
using Ecommerce.Dtos.Order;
using Ecommerce.Dtos.PaymentDetailsDto;
using Ecommerce.Dtos.Product;
using Ecommerce.Dtos.Service;
using Ecommerce.infrastruction;
using Ecommerce.Repository;

namespace Ecommerce.Services
{
    public interface IProductService
    {
        Task<ICollection<GetProduct>> GetAllAsync();
        Task<GetProduct> GetByIdAsync(int id);

        Task<ServiceResponse> Add(CreateProduct e);

        Task<ServiceResponse> Delete(int id);

        Task<ServiceResponse> Update(UpdateProduct e);


      
    }


    public  class ProductService(IMapper mapper, IProductRepo repo,IUploadImageService imageServ) : IProductService
    {
        public async Task<ServiceResponse> Add(CreateProduct e)
        {
            var res=mapper.Map<Product>(e);


            if(e.Image is not null)
            {



               var response=await  imageServ.UploadImage(new UploadImageDto { Entity = "Products", Image = e.Image });
                if (response.ImageUrl is null)
                    return new ServiceResponse { succeeded = false, msg = response.Msg };


                res.ImagePath = response.ImageUrl;

            }

            repo.Add(res);
            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "Product is created")
            : new ServiceResponse(false, "Product couldnt be created");

        }

        public async Task<ServiceResponse> Delete(int id)
        {
            var res = await repo.GetByIdAsync(id);
            repo.Delete(res);
            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "PaymentDetails is deleted")
            : new ServiceResponse(false, "PaymentDetails couldnt be deleted");


        }

        public async Task<ICollection<GetProduct>> GetAllAsync()
        {
            var res = await repo.GetAllAsync();
            return mapper.Map<ICollection<GetProduct>>(res);
        }

        public async Task<GetProduct> GetByIdAsync(int id)
        {
            var res = await repo.GetByIdAsync(id);
            return mapper.Map<GetProduct>(res);
        }

        public async Task<ServiceResponse> Update(UpdateProduct e)
        {



            var res = mapper.Map<Product>(e);


            if (e.Image is not null)
            {



                var response = await imageServ.UploadImage(new UploadImageDto { Entity = "Products", Image = e.Image });
                if (response.ImageUrl is null)
                    return new ServiceResponse { succeeded = false, msg = response.Msg };


                res.ImagePath = response.ImageUrl;

            }



            repo.Update(res);

            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "PaymentDetails is updated")
            : new ServiceResponse(false, "PaymentDetails couldnt be updated");

        }

      
    }
}

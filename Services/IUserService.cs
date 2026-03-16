using AutoMapper;
using Ecommerce.Dtos.Product;
using Ecommerce.infrastruction;
using Ecommerce.Dtos.Service;
using Ecommerce.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Ecommerce.Dtos.Image;

namespace Ecommerce.Services
{
    public interface IUserService
    {

       
        Task<GetUser> GetByIdAsync(string id);

        Task<GetProfile> GetUserProfile(string id);

        Task<ServiceResponse> Update(UpdateUser e);





    }



    public class UserService(IMapper mapper, UserManager<User> userManager,IUploadImageService imageServ) : IUserService
    {
     
   
        public async Task<GetUser> GetByIdAsync(string id)
        {
            var res = await userManager.FindByIdAsync(id);
            return mapper.Map<GetUser>(res);
        }

        public async Task<GetProfile> GetUserProfile(string id)
        {
            var res = await userManager.FindByIdAsync(id);
            return mapper.Map<GetProfile>(res);
        }

        public async Task<ServiceResponse> Update(UpdateUser e)
        {
            var res =await userManager.FindByIdAsync(e.Id);

            if(res==null) new ServiceResponse(false, "profile couldnt be found");
            res.FName = e.FName;
            res.LName = e.LName;
            res.PhoneNumber = e.PhoneNumber;
           

            if (e.Image is not null)
            {



                var response = await imageServ.UploadImage(new UploadImageDto { Entity = "Users", Image = e.Image });
                if (response.ImageUrl is null)
                    return new ServiceResponse { succeeded = false, msg = response.Msg };


                res.ProfilePicPath = response.ImageUrl;

            }

            var upRes = await userManager.UpdateAsync(res);


            return upRes.Succeeded ? new ServiceResponse(true, "Profile is  updated")
                : new ServiceResponse(false, "profile couldnt be updated");
            
        }
    }
}

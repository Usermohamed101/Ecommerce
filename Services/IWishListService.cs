using AutoMapper;
using Ecommerce.Dtos.Product;
using Ecommerce.Dtos.Service;
using Ecommerce.Dtos.WishListDto;
using Ecommerce.Dtos.WishListItem;
using Ecommerce.infrastruction;
using Ecommerce.Repository;
using System.Reflection.Metadata.Ecma335;

namespace Ecommerce.Services
{
    public interface IWishListService
    {
       // Task<ICollection<GetWishList>> GetAllAsync();
        Task<GetWishList> GetByIdAsync(int id);
        Task<GetWishList> GetByUserIdAsync(string id);

        Task<ServiceResponse> AddItem(string userId,int itemId);

        Task<ServiceResponse> DeleteItem(string userId,int id);

        Task<ServiceResponse> ClearList(string userId);
    }


    public class WishListService(IMapper mapper, IWishListRepo repo) : IWishListService
    {
        public async Task<ServiceResponse> AddItem(string userId,int productId)
        {
            var wishList = await repo.GetByUserIdAsync(userId);
            if (wishList is null) {
                wishList = new WishList() { UserId = userId };
                repo.Add(wishList);
                }
            var listItem=new WishListItem() { ProductId = productId, WishlistId = wishList.Id };
            wishList.Items.Add(listItem);
            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "item is added") : new ServiceResponse(false, "item couldnt be added");

        }

        public async Task<ServiceResponse> ClearList(string userId)
        {
            var wishList = await repo.GetByUserIdAsync(userId);
            if (wishList is null) return new ServiceResponse(false, "list could not be found");

            wishList.Items.Clear();

            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "list is cleared") : new ServiceResponse(false, "list couldnt be cleared");

        }

        public async Task<ServiceResponse> DeleteItem(string userId,int productId)
        {
            var wishList = await repo.GetByUserIdAsync(userId);
            if (wishList is null) return new ServiceResponse(false, "list could not be found");

            var i = wishList.Items.FirstOrDefault(c => c.ProductId == productId);
            wishList.Items.Remove(i);

            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "item is deleted") : new ServiceResponse(false, "item couldnt be deleted");


        }

        public async Task<GetWishList> GetByIdAsync(int id)
        {
            var res = await repo.GetByIdAsync(id);
            return mapper.Map<GetWishList>(res);
        }
        public async Task<GetWishList> GetByUserIdAsync(string id)
        {
            var res = await repo.GetByUserIdAsync(id);
            return mapper.Map<GetWishList>(res);
        }
    }
}

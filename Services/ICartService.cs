using AutoMapper;
using Ecommerce.Dtos.Cart;
using Ecommerce.Dtos.CartItem;
using Ecommerce.Dtos.Category;
using Ecommerce.Dtos.Service;
using Ecommerce.infrastruction;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ecommerce.Services
{
    public interface ICartService
    {


        Task<GetCart> GetByIdAsync(int id);

        Task<GetCart> GetByUserIdAsync(string id);


        Task<ServiceResponse> ClearList(string userId);

        Task<ServiceResponse> AddItemAsync(string userId, CreateCartItem cartItem);
        Task<ServiceResponse> UpdateItemAsync(string userId, UpdateCartItem cartItem);

        Task<ServiceResponse> DeleteItemAsync(string userId, int product);
    }




    public class CartService(ICartRepo repo, IMapper mapper) : ICartService
    {
        public async Task<ServiceResponse> AddItemAsync(string userId, CreateCartItem cartItemDto)
        {

            var cart =await repo.GetByUserIdAsync(userId);
            if (cart == null) {
                cart = new Cart() { UserId = userId };
                repo.Add(cart);
                await repo.SaveChangesAsync();
                };

            var cartItem = new CartItem() { CartId = cart.Id, ProductId = cartItemDto.ProductId, Quantity = cartItemDto.Quantity };

            cart.Items.Add(cartItem);
            repo.Update(cart);
           return await repo.SaveChangesAsync()>0?new ServiceResponse(true,"item is added"): new ServiceResponse(true, "item couldnt be added");
            



        }
        public async Task<ServiceResponse> ClearList(string userId)
        {
            var carList = await repo.GetByUserIdAsync(userId);
            if (carList is null) return new ServiceResponse(false, "list could not be found");

            carList.Items.Clear();

            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "list is cleared") : new ServiceResponse(false, "list couldnt be cleared");

        }

        public async Task<ServiceResponse> DeleteItemAsync(string userId, int product)
        {
            var cart = await repo.GetByUserIdAsync(userId);
            if (cart is null) return new ServiceResponse(false, "cart couldnt be found");

            var item = cart.Items.FirstOrDefault(c => c.ProductId == product);

            cart.Items.Remove(item);

            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "item is deleted") : new ServiceResponse(true, "item couldnt be deleted");



        }

        public async Task<GetCart> GetByIdAsync(int id)
        {
            var res = await repo.GetByIdAsync(id);
            return res is not null? mapper.Map<GetCart>(res):null;
        }

        public async Task<ServiceResponse> UpdateItemAsync(string userId, UpdateCartItem cartItem)
        {
            var cart = await repo.GetByUserIdAsync(userId);
            if (cart is null) return new ServiceResponse(false, "cart couldnt be found");

            var item = cart.Items.FirstOrDefault(c => c.ProductId==cartItem.ProductId );
            item.Quantity = cartItem.Quantity;

            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "item is updated") : new ServiceResponse(true, "item couldnt be updated");


        }

        public async Task<GetCart> GetByUserIdAsync(string id)
        {



            var cart = await repo.GetByUserIdAsync(id);
            return mapper.Map<GetCart>(cart);
        }
    }
}

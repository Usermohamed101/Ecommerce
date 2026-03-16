using AutoMapper;
using Ecommerce.Dtos.Category;
using Ecommerce.Dtos.Order;
using Ecommerce.Dtos.Service;
using Ecommerce.infrastruction;
using Ecommerce.Models;
using Ecommerce.Repository;

namespace Ecommerce.Services
{
    public interface IOrderService
    {
        Task<ICollection<GetOrder>> GetAllAsync();

        Task<ICollection<GetOrder>> GetUserAllOrderAsync(string userId);
        Task<GetOrder> GetByIdAsync(string userId,int id);

        Task<GetOrder> GetByIdAsync( int id);

        Task<ServiceResponse> CreateOrder(string userId,CreateOrder order);

        Task<ServiceResponse> Delete(int id);

        Task<ServiceResponse> Update(UpdateOrder e);

        Task<ServiceResponse> UpdateOrderStatusAsync( int orderId, OrderStatus status);
    }


    public class OrderService(IMapper mapper, IOrderRepo repo,ICartRepo cartRepo,ICartService cartService) : IOrderService
    {
        public async Task<ServiceResponse> CreateOrder(string userId,Dtos.Order.CreateOrder orderDto)
        {
       
            
            var cart = await cartRepo.GetByUserIdAsync(userId);

             if (cart is null || cart.Items.Count == 0) throw new Exception("the cart is Empty");

            var order =new Order() { UserId=userId};
            

           

            
            foreach(var item in cart.Items)
            {
                if (item.Quantity > item.Product.StockQuantity) throw new Exception("there is no enough quantity");

                order.Items.Add(new OrderItem()
                {
                    ProductId=item.ProductId,
                    Quantity=item.Quantity,
                    UnitPrice=item.Product.Price,

                    
                });
                 
                
            }
            order.TotalPrice = order.GetTotalPrice();
            order.ShippingAddressId = orderDto.ShippingAddressId;
            order.DeliveryOptionId = orderDto.DeliveryOptionId;

            repo.Add(order);
            var res =await repo.SaveChangesAsync();

            if (res > 0)
            {
               await cartService.ClearList(userId);
             
                return new ServiceResponse(true, "oredr is created");
            }
            else
            {
              return new ServiceResponse(false, "order couldnt be created");

            }
          
               

        }



         public async Task<ServiceResponse> UpdateOrderStatusAsync( int orderId, OrderStatus status)
        {

            var order = (await repo.GetAllAsync()).FirstOrDefault(o=> o.Id == orderId);

            order.Status = status;

            repo.Update(order);

            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "oredr status is updated")
                : new ServiceResponse(false, "order status couldnt be updated");

        }


        public async Task<ServiceResponse> Delete(int id)
        {

            var res = await repo.GetByIdAsync(id);

            repo.Delete(res);
            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "oredr is deleted")
                : new ServiceResponse(false, "order couldnt be deleted");

        }

        public async Task<ICollection<GetOrder>> GetAllAsync()
        {
            var res = await repo.GetAllAsync();
            return mapper.Map< ICollection<GetOrder>>(res);
        }


       public async Task<ICollection<GetOrder>> GetUserAllOrderAsync(string userId)
        {

            var res = (await repo.GetAllAsync()).Where(o => o.UserId == userId).ToList();
            if (res is null || res.Count == 0) throw new Exception("there are no orders");

            return mapper.Map<List<GetOrder>>(res);
        }


        public async Task<GetOrder> GetByIdAsync(string userId,int id)
        {
            var res = (await repo.GetAllAsync()).FirstOrDefault(o => o.UserId == userId&&o.Id==id);
            return mapper.Map<GetOrder>(res);
        }

        public async Task<ServiceResponse> Update(UpdateOrder e)
        {
            var res = mapper.Map<Order>(e);
            repo.Update(res);

            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "oredr is updated")
                : new ServiceResponse(false, "order couldnt be updated");

        }

        public async Task<GetOrder> GetByIdAsync(int id)
        {
            var res = (await repo.GetAllAsync()).FirstOrDefault(o =>  o.Id == id);
            return mapper.Map<GetOrder>(res);
        }
    }
}

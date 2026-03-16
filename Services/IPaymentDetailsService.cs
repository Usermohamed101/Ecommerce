using AutoMapper;
using Ecommerce.Dtos.Fawaterak;
using Ecommerce.Dtos.Order;
using Ecommerce.Dtos.PaymentDetailsDto;
using Ecommerce.Dtos.Service;
using Ecommerce.infrastruction;
using Ecommerce.Models;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public interface IPaymentDetailsService
    {


        Task<ICollection<GetPaymentDetails>> GetAllAsync();
        Task<GetPaymentDetails> GetByIdAsync(int id);

        Task<ServiceResponse> Add(CreatePaymentDetails e);

        Task<ServiceResponse> Delete(int id);
        Task<List<GetPaymentDetails>> GetAllUserPaymentDetails(string id);


        Task<GetPaymentMethodsRequest> GetPaymentMethodsAsync();
        Task<PaymentResponse> ExecutePaymentAsync(string userId, ExecutePaymentRequest req);

    }



    public class PaymentService(IPaymentDetailsRepo repo,IOrderRepo orderRepo, IMapper mapper,IFawaterakService fawaterak,UserManager<User> userRepo) : IPaymentDetailsService
    {
        public async Task<ServiceResponse> Add(CreatePaymentDetails e)
        {

            var order = await orderRepo.GetByIdAsync(e.OrderId);
            if (order is null) return new ServiceResponse(false, "order not found");

            var res =new PaymentDetails() { 
            Amount=e.Amount,
            OrderId=e.OrderId,
            PaymentMethod=e.PaymentMethod,
            };
            order.Status = Models.OrderStatus.Delivered;
            repo.Add(res);
            orderRepo.Update(order);
            repo.Update(res);
            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "PaymentDetails is created")
            : new ServiceResponse(false, "PaymentDetails couldnt be created");


        }

        public async Task<ServiceResponse> Delete(int id)
        {
            var res = await repo.GetByIdAsync(id);
            repo.Delete(res);
            return await repo.SaveChangesAsync() > 0 ? new ServiceResponse(true, "PaymentDetails is deleted")
            : new ServiceResponse(false, "PaymentDetails couldnt be deleted");

        }

        public async Task<ICollection<GetPaymentDetails>> GetAllAsync()
        {
            var res = await repo.GetAllAsync();
            return mapper.Map<ICollection<GetPaymentDetails>>(res);
        }

        public async Task<GetPaymentDetails> GetByIdAsync(int id)
        {
            var res = await repo.GetByIdAsync(id);
            return mapper.Map<GetPaymentDetails>(res);
        }

    
        public async Task<List<GetPaymentDetails>> GetAllUserPaymentDetails(string id)
        {
            var res = (await repo.GetAllAsync()).Where(p => p.Order.UserId == id).ToList();
            return mapper.Map<List<GetPaymentDetails>>(res);
        }


       public async Task<GetPaymentMethodsRequest> GetPaymentMethodsAsync()
        {

            return await fawaterak.GetPaymentMethods();
        }

        public async Task<PaymentResponse> ExecutePaymentAsync(string userId,ExecutePaymentRequest req)
        {
            var order =await  orderRepo.GetOrderWithItemsAsync(req.OrdrerId);

           

            var user = await userRepo.FindByIdAsync(userId);

            var cartItems = new List<Dtos.Fawaterak.CartItem>();
            foreach(var i in order.Items)
            {
                var carIt = new Dtos.Fawaterak.CartItem()
                {
                    Name = i.Product.Name,
                    Price = i.UnitPrice,
                    Quantity = i.Quantity,
                };
                cartItems.Add(carIt);
            }
            var invoice = new EInvoiceRequest()
            {
                PaymentMethodId = req.PaymentMethodId,
                CartTotal = order.GetTotalPrice(),
                Currency = "EGP",
                Customer = new Customer()
                {
                    FName = user.FName,
                    LName = user.LName,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Address = "address"
                },
                Urls = new RedirectionUrls()
                {
                    SuccessfulUrl = "https://mydomain/success",
                    FailUrl = "https://mydomain/failed",
                    PendingUrl = "https://mydomain/pending"
                },
                CartItems = cartItems,

                PayLoad = new payLoad { orderId = order.Id }
            };
return  await fawaterak.ExecutePaymentAsync(invoice);
        }
    }

}

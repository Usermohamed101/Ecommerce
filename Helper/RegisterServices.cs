using Castle.Core.Smtp;
using Ecommerce.infrastruction;
using Ecommerce.Repository;
using Ecommerce.Services;
using System.Runtime.CompilerServices;
using static Ecommerce.Services.IDeliveryOptionsService;

namespace Ecommerce.Helper
{
    public static class RegisterServices
    {

        
       public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFawaterakService, FawaterakService>();
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPaymentDetailsService, PaymentService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDeliveryOptionsService, DeliveryOptionService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IWishListService, WishListService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUploadImageService, UploadImageService>();
            services.AddHttpClient();

            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IPaymentDetailsRepo, PaymentDetailsRepo>();
            services.AddScoped<ICartRepo, CartRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IDeliveryOptionRepo, DeliveryOptionRepo>();
            services.AddScoped<IReviewRepo, ReviewRepo>();
            services.AddScoped<IWishListRepo, WishListRepo>();
           

            services.AddTransient<IEmailSender, SendEmailService>();


            return services;
        } 


    }
}

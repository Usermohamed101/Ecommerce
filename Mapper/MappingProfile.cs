using AutoMapper;
using Ecommerce.Dtos.Address;
using Ecommerce.Dtos.Cart;
using Ecommerce.Dtos.CartItem;
using Ecommerce.Dtos.DeliveryOption;
using Ecommerce.Dtos.Order;
using Ecommerce.Dtos.OrderItem;
using Ecommerce.Dtos.PaymentDetailsDto;
using Ecommerce.Dtos.Product;
using Ecommerce.Dtos.Review;
using Ecommerce.Dtos.WishListDto;
using Ecommerce.Dtos.WishListItem;
using Ecommerce.infrastruction;

namespace Ecommerce.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //product
            CreateMap<Product, GetProduct>();
            CreateMap<CreateProduct, Product>();
            CreateMap<UpdateProduct, Product>();



            //cart
            CreateMap<CreateCart,Cart>();
            CreateMap<UpdateCart, Cart>();
            CreateMap<Cart, GetCart>();


            //paymentDetails
            CreateMap<PaymentDetails, GetPaymentDetails>();
            CreateMap<CreatePaymentDetails, PaymentDetails>();

            //order

            CreateMap<CreateOrder,Order>();
            CreateMap<UpdateOrder, Order>();
            CreateMap<Order, GetOrder>();


            //review

            CreateMap<Review, GetReview>();
            CreateMap<CreateReview, Review>();
            CreateMap<UpdateReview, Review>();

            //wishlist
            CreateMap<WishList, GetWishList>();
            CreateMap<CreateWishList, WishList>();
            CreateMap<UpdateWishList, WishList>();

            //address
            CreateMap<Address, GetAddress>();
            CreateMap<CreateAddress, Address>();
            CreateMap<UpdateAddress, Address>();

            //DeliverOption
            CreateMap<DeliveryOption, GetDeliveryOption>();
            CreateMap<UpdateDeliveryOption, DeliveryOption>();
            CreateMap<CreateDeliveryOption, DeliveryOption>();

            //orderItem
            CreateMap<OrderItem, GetOrderItem>();
            CreateMap<CreateOrderItem, OrderItem>();
            CreateMap<UpdateOrderItem, OrderItem>();


            //wishListItem

            CreateMap<WishListItem, GetWishListItem>();
            CreateMap<CreateWishListItem, WishListItem>();

            //cartItem
            CreateMap<CartItem, GetCartItem>();
            CreateMap<CreateCartItem, CartItem>();
            CreateMap<UpdateCartItem, CartItem>();






        }

    }
}

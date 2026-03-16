using Ecommerce.Dtos.Order;
using Ecommerce.infrastruction;

namespace Ecommerce.Repository
{
    public interface IOrderRepo:IGeneric<Order,int>
    {
        Task<Order> GetOrderWithItemsAsync(int id);
    }
}

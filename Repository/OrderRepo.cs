using Ecommerce.Dtos.Order;
using Ecommerce.infrastruction;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class OrderRepo(ECommerceContext contxt) : GenericRepo<Order, int>(contxt), IOrderRepo
    {
      public  async Task<Order> GetOrderWithItemsAsync(int id)
        {
            return await contxt.Orders.Include(o => o.Items).ThenInclude(o=>o.Product).FirstOrDefaultAsync(o=>o.Id==id);
        }
    }
}

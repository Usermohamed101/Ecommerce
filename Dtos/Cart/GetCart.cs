using Ecommerce.Dtos.CartItem;

namespace Ecommerce.Dtos.Cart
{
    public class GetCart:CartBase
    {
        public int Id { get; set; }
        public ICollection<GetCartItem> Items { get; set; } = new List<GetCartItem>();
    }
}

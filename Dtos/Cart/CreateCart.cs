using Ecommerce.Dtos.CartItem;

namespace Ecommerce.Dtos.Cart
{
    public class CreateCart:CartBase
    {
        public ICollection<CreateCartItem> Items { get; set; } = new List<CreateCartItem>();


    }
}

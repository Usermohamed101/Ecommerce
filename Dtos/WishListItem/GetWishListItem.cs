using Ecommerce.Dtos.Product;

namespace Ecommerce.Dtos.WishListItem
{
    public class GetWishListItem:WishListItemBase
    {

        public int Id { get; set; }
        public GetProduct product { get; set; }
       

    }
}

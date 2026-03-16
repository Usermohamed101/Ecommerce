using Ecommerce.Dtos.WishListItem;

namespace Ecommerce.Dtos.WishListDto
{
    public class CreateWishList:WishListBase
    {
        public ICollection<CreateWishListItem> Items { get; set; } = new List<CreateWishListItem>();
    }
}

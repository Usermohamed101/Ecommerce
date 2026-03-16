using Ecommerce.Dtos.WishListItem;

namespace Ecommerce.Dtos.WishListDto
{
    public class GetWishList:WishListBase
    {
        public ICollection<GetWishListItem> Items { get; set; } = new List<GetWishListItem>();

    }
}

namespace Ecommerce.infrastruction
{
    public class WishList
    {


   
        
            public int Id { get; set; }
            public string UserId { get; set; }
            public User User { get; set; }

            public ICollection<WishListItem> Items { get; set; } = new List<WishListItem>();
        

            public void AddItem(int productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                Items.Add(new WishListItem(productId, Id));

            }
        }

        public void RemoveItem(int productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                Items.Remove(item);
            }


        }


    }
}

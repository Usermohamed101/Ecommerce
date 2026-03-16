namespace Ecommerce.infrastruction
{
    public class Cart
    {



      
            public int Id { get; set; }
            public string UserId { get; set; }
            public User User { get; set; }

            public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        //public bool Add(int productId,int quantity)
        //{
        //    if (quantity <= 0)
        //    {
        //        return false;
        //    }
        //    var item = Items.FirstOrDefault(i => i.ProductId == productId);

        //    if (item == null)
        //    {
        //        item = new CartItem(Id,productId) ;
        //        item.SetQuantity(quantity);
        //        Items.Add(item);

        //        return true;
        //    }
        //    else
        //    {
        //        item.IncreaseQuantity(quantity);
               
        //    }
        //     return false;
        //}

        //public bool Remove(int productId)
        //{
        //    var item = Items.FirstOrDefault(i => i.Id == productId);
        //    if (item == null) return false;

        //     return  Items.Remove(item);
           

        //}





    }
}

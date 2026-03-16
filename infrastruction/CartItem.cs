namespace Ecommerce.infrastruction
{
    public class CartItem
    {

        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart? Cart { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; } = 0;

        //public CartItem(int cartId, int productId)
        //{
        //    CartId = cartId;
        //    ProductId = productId;
        //}
        //public void IncreaseQuantity(int quantity)
        //{
        //    if (quantity > 0)
        //    {
        //        Quantity += quantity;
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Quantity must be more than 0");
        //    }
        //}

        //public void SetQuantity(int quantity)
        //{
        //    if (quantity >= 0)
        //    {
        //        Quantity = quantity;
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Quantity must be 0 or more");
        //    }

        //}

    }
}

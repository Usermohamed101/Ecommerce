namespace Ecommerce.infrastruction
{
    public class OrderItem
    {


        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get;  set; }
        public decimal UnitPrice { get; set; }

        public decimal CalculateTotalPrice => Quantity * UnitPrice;

        //private OrderItem()
        //{
            
        //}
        //public OrderItem(int orderId,int productId,decimal unitPrice)
        //{
        //    UnitPrice = unitPrice;
        //    OrderId=orderId;
        //    ProductId = productId;
        //}
        //public void Increase(int quantity)
        //{
        //    if (quantity <= 0)
        //    {
        //        throw new ArgumentException("quantity should be more than 0");
        //    }

        //    SetQuantity(Quantity + quantity);
        //}



        //public void SetQuantity(int quantity)
        //{
        //    if (quantity < 0)
        //    {
        //        throw new ArgumentException("quantity should be 0 or more");
        //    }

        //   this.Quantity= quantity;

        //}

       
       


    }
}
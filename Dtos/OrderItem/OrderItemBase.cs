namespace Ecommerce.Dtos.OrderItem
{
    public class OrderItemBase
    {
       
        public int OrderId { get; set; }
        
        public int ProductId { get; set; }
       

        public int Quantity { get; private set; }
        public decimal UnitPrice { get; set; }

        public decimal CalculateTotalPrice => Quantity * UnitPrice;

    }
}

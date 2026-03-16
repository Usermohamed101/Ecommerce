namespace Ecommerce.Models
{
    public enum OrderStatus
    {

        Pending = 0,
        Processing = 1,
        Shipped = 2,
        Delivered = 3,
        Cancelled = 4,
        Returned = 5,
        Refunded = 6,
        Paid=7
    }
}
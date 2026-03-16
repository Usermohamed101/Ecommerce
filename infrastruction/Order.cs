using Ecommerce.Models;
using System.Data.SqlTypes;
using System.Net;

namespace Ecommerce.infrastruction
{
    public class Order
    {


        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending; 

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        public decimal TotalPrice { get; set; }
        public int ShippingAddressId { get; set; }
        public Address ShippingAddress { get; set; }

        public int DeliveryOptionId { get; set; }
        public DeliveryOption DeliveryOption { get; set; }

        public virtual ICollection<PaymentDetails> PaymentDetails { get; set; } = new List<PaymentDetails>();


        public decimal GetTotalPrice()=>Items.Sum(i => i.UnitPrice * i.Quantity);

        //public void AddItem(int productId,int quantity,decimal unitPrice)
        //{
        //    if (quantity <= 0)
        //        throw new ArgumentException("Quantity must be greater than zero");

        //    if (unitPrice < 0)
        //        throw new ArgumentException("Unit price cannot be negative");

        //    var item = Items.FirstOrDefault(i => i.ProductId == productId);
        //    if (item == null)
        //    {

        //        item = new OrderItem(Id,productId,unitPrice) ;
        //        item.SetQuantity(quantity);
        //        Items.Add(item);
        //    }
        //    else
        //    {

        //        item.Increase(quantity);

        //    }

        //}

        //public void RemoveItem(int productId)
        //{
        //    var item = Items.FirstOrDefault(i => i.ProductId == productId);
        //    if (item != null)
        //    {
        //        Items.Remove(item);
        //    }


        //}

        //public void SetStatus(OrderStatus status)
        //{
        //    this.Status = status;
        //}

        //public void CalculateTotalPrice()
        //{

        //    TotalPrice = Items.Sum(i => i.CalculateTotalPrice);

        //}

        //public void AddPaymentDetails(decimal amount,PaymentMethods paymentMethod)
        //{

        //    if (amount < 0) throw new InvalidOperationException("operation must be positive");
        //    PaymentDetails.Add(new PaymentDetails(Id, amount, paymentMethod));

        //}
    }
}
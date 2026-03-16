
namespace Ecommerce.infrastruction
{
    public class DeliveryOption
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal DeliveryPrice { get; set; }

        public int EstimatedDeliveryDays { get; set; }

        public bool isAvailable { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

    }
}

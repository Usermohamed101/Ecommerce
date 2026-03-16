namespace Ecommerce.Dtos.DeliveryOption
{
    public class DeliveryOptionBase
    {

      

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal DeliveryPrice { get; set; }

        public int EstimatedDeliveryDays { get; set; }

        public bool isAvailable { get; set; }

   

    }
}

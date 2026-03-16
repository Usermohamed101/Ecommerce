using Ecommerce.infrastruction;

namespace Ecommerce.Repository
{
    public class DeliveryOptionRepo(ECommerceContext contxt) : GenericRepo<DeliveryOption, int>(contxt), IDeliveryOptionRepo
    {

    }
}

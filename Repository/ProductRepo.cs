using Ecommerce.infrastruction;

namespace Ecommerce.Repository
{
    public class ProductRepo(ECommerceContext contxt) : GenericRepo<Product, int>(contxt), IProductRepo
    {

    }

}

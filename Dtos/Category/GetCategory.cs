
using Ecommerce.Dtos.Product;

namespace Ecommerce.Dtos.Category
{
    public class GetCategory:CategoryBase
    {

        public int Id { get; set; }
       
        public ICollection<GetProduct> Products { get; set; } = new List<GetProduct>();
    }
}

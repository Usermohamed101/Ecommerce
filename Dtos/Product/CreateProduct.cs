namespace Ecommerce.Dtos.Product
{
    public class CreateProduct:ProductBase
    {
        public IFormFile? Image { get; set; }
    }
}

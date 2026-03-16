namespace Ecommerce.Dtos.Product
{
    public class UpdateProduct:ProductBase
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }
    }
}

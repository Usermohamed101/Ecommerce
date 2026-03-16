namespace Ecommerce.Dtos.Product
{
    public class GetProduct:ProductBase
    {
        public int Id { get; set; }

        public string ImagePath { get; set; } = string.Empty;
    }
}

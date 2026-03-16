using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dtos.Product
{
    public class ProductBase
    {

        [Required(ErrorMessage ="Name is required")]
        [StringLength(100,MinimumLength =3,ErrorMessage ="Name must be 3-100 characters")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required")]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Description must be 3-300 characters")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
      
     
        [Required(ErrorMessage = "StockQuantity is required")]
        public int StockQuantity { get; set; }
        public long Version { get; set; }
        public int CategoryId { get; set; }
    }
}

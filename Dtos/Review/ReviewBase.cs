using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dtos.Review
{
    public class ReviewBase
    {

        [Required(ErrorMessage = "ProductId must be included")]
        public int ProductId { get; set; }

      
        public string? UserId { get; set; }
        [Range(1,5)]      
        
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;


    }
}

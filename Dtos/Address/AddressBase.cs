using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dtos.Address
{
    public class AddressBase
    {

        [Required]  
        
        public string UserId { get; set; }
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Street { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^\d{4,8}$")]
        public string PostalCode { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;

    }
}

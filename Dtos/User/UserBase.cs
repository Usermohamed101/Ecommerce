using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dtos.User
{
    public class UserBase
    {
        [Required(ErrorMessage ="FName is required")]
        [StringLength(50,MinimumLength =1,ErrorMessage ="FName must be 1-50 characters")]
        public string? FName { get; set; }
        [Required(ErrorMessage = "LName is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "LName must be 1-50 characters")]
        public string? LName { get; set; }
       // [EmailAddress]
        public string? Email { get; set; }
        [StringLength(12,MinimumLength =12,ErrorMessage ="PhoneNumber must be 12 characters")]
        public string? PhoneNumber { get; set; }
    

       

    }
}

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dtos.Auth
{
    public class LogInDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
//        [RegularExpression(
//    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$%&^*!]).{8,}$",
//    ErrorMessage = "Password must be at least 8 characters and include upper, lower, number, and special character."
//)]
        public string? Password { get; set; }

    }
}

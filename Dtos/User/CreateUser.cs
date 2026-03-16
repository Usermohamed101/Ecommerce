using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dtos.User
{
    public class CreateUser:UserBase
    {

//        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
//ErrorMessage = "Password must be at least 8 characters and include upper, lower, number, and special character."
//)]
        public string? Password { get; set; }
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        public IFormFile? Image { get; set; }

    }
}

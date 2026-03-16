using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dtos.User
{
    public class GetProfile
    {

        public string? FName { get; set; }
   
        public string? LName { get; set; }
     
        public string? Email { get; set; }


        public string ProfilePicPath { get; set; } = string.Empty;

    }
}

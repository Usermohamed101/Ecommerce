using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.infrastruction
{
    public class User:IdentityUser
    {

        public string? FName { get; set; }

        public string? LName { get; set; } 



        public string ProfilePicPath { get; set; } = string.Empty;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public WishList? Wishlist { get; set; }
        public Cart? Cart { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; } = new();

    }
}

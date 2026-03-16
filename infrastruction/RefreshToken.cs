using Microsoft.EntityFrameworkCore;

namespace Ecommerce.infrastruction
{
    [Owned]
    public class RefreshToken
    {


       public string Token { get; set; }

        public DateTime ExpiresOn { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public DateTime? RevokedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsActive => RevokedAt == null && !IsExpired;
    }
}

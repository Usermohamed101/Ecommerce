using System.Text.Json.Serialization;

namespace Ecommerce.Dtos.Auth
{
    public class AuthResultDto
    {

        public string Token { get; set; }

      //  public DateTime ExpiresAt { get; set; }

        public string UserId { get; set; }

        public string Email { get; set; }

        public string Msg { get; set; }

        public bool IsAuthenticated { get; set; } = false;

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }

     
      
    }
}

namespace Ecommerce.Dtos.Auth
{
    public class ForgotPasswordResponse
    {

        public string? Msg { get; set; }

        public string? Token { get; set; }
        public string? Email { get; set; }

        public bool IsSucceded { get; set; }

    }
}

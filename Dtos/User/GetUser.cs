namespace Ecommerce.Dtos.User
{
    public class GetUser:UserBase
    {
        public string Id { get; set; }
        public string ProfilePicPath { get; set; } = string.Empty;
    }
}

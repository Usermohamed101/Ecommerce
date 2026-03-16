namespace Ecommerce.Dtos.User
{
    public class UpdateUser:UserBase
    {
       public string Id { get; set; }
        public IFormFile? Image { get; set; }

    }
}

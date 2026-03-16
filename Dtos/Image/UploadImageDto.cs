namespace Ecommerce.Dtos.Image
{
    public class UploadImageDto
    {
        public string Entity { get; set; }

        public IFormFile? Image { get; set; }
    }
}

using Ecommerce.Dtos.Image;

namespace Ecommerce.Services
{
    public interface IUploadImageService
    {


        Task<UploadImageResponse> UploadImage(UploadImageDto e);

    }


    public class UploadImageService(IWebHostEnvironment env) : IUploadImageService
    {
      


        public async Task<UploadImageResponse> UploadImage(UploadImageDto e)
        {
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            var ex = Path.GetExtension(e.Image.FileName);


            if (!allowed.Contains(ex)) return new UploadImageResponse { status = false, Msg = "this extension is not supproted" };
            if (!(e.Image.Length > 0) || e.Image.Length > 5 * 1024 * 1024) return new UploadImageResponse { status = false, Msg = "the size of the image doesnt fit make sure it is 5 MB" };

            var path = Path.Combine(env.WebRootPath, $"Images/{e.Entity}");

            if (!Directory.Exists(path)) return new UploadImageResponse { status = false, Msg = "directory is not found" };

            string fileName = $"{Guid.NewGuid()}{ex}";
            var fullPath = Path.Combine(path, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);

            await e.Image.CopyToAsync(stream);

            string url = $"/Images/{e.Entity}/{fileName}";

            return new UploadImageResponse { status = true, ImageUrl = url };


        }
    }
}

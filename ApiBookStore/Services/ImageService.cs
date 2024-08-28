using ApiBookStore.Interfaces;

namespace ApiBookStore.Services
{
    public class ImageService : IImageService
    {
        public string ConvertImage(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(fileBytes);

                return base64String;
            }
        }
    }
}

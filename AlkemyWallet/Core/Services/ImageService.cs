using System.Text.RegularExpressions;
using AlkemyWallet.Core.Interfaces;

namespace challenge.Services
{
    public class ImageService : IImageService
    {
        public enum ImageType
        {
            Catalogue 
        }

        public async Task<string> StoreImage(IFormFile imageFile, ImageType imageType)
        {
            var regex = new Regex(".*\\.jpg|.*\\.png|.*\\.pdf");

            if (!regex.IsMatch(imageFile.FileName))
                throw new Exception("El formato de imagen no es admitido");

            if (imageFile.Length / 1024.0 / 1024.0 > 10)
                throw new Exception("La imagen debe ser menor a 10MB");

            var directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Storage");

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fileHeader = imageType switch
            {
                ImageType.Catalogue => "Catalogue",              
                _ => "Unknown"
            };

            var guid = Guid.NewGuid(); 

            var filePath = $"{fileHeader}-{guid}-{imageFile.FileName}";

            using var stream = File.Create(Path.Combine(directoryPath, filePath));

            await imageFile.CopyToAsync(stream);

            return filePath;
        }

    }
}

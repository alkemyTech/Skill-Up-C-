using challenge.Services;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IImageService
    {
        Task<string> StoreImage(IFormFile imageFile, ImageService.ImageType imageType);
    }
}
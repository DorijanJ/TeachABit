using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace TeachABit.Service.Util.Images
{
    public class ImageManipulationService : IImageManipulationService
    {
        public async Task<MemoryStream> ConvertToProfilePhoto(IFormFile image)
        {
            using var stream = image.OpenReadStream();
            var loadedImage = await Image.LoadAsync(stream);

            var pfpResolution = new Size(256, 256);

            int cropWidth = loadedImage.Width;
            int cropHeight = loadedImage.Height;

            if (cropWidth > cropHeight)
            {
                cropWidth = cropHeight;
            }
            else if (cropHeight > cropWidth)
            {
                cropHeight = cropWidth;
            }

            var cropX = (loadedImage.Width - cropWidth) / 2;
            var cropY = (loadedImage.Height - cropHeight) / 2;
            loadedImage.Mutate(x => x.Crop(new Rectangle(cropX, cropY, cropWidth, cropHeight)));

            loadedImage.Mutate(x => x.Resize(pfpResolution));

            var memoryStream = new MemoryStream();
            await loadedImage.SaveAsync(memoryStream, new JpegEncoder { Quality = 75 });

            return memoryStream;
        }
    }
}

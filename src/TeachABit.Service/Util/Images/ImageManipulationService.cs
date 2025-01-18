using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TeachABit.Service.Util.Images
{
    public class ImageManipulationService : IImageManipulationService
    {
        public async Task<MemoryStream> ConvertToNaslovnaSlika(string base64Image)
        {
            string[] pd = base64Image.Split(',');
            byte[] imageBytes = Convert.FromBase64String(pd[1]);
            using var stream = new MemoryStream(imageBytes);
            var loadedImage = await Image.LoadAsync(stream);

            var imageResolution = new Size(1024, 512);

            loadedImage.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = imageResolution,
                Mode = ResizeMode.Crop
            }));

            var memoryStream = new MemoryStream();
            await loadedImage.SaveAsync(memoryStream, new JpegEncoder { Quality = 100 });

            return memoryStream;
        }

        public async Task<MemoryStream> ConvertToProfilePhoto(string base64Image)
        {
            string[] pd = base64Image.Split(',');
            byte[] imageBytes = Convert.FromBase64String(pd[1]);
            using var stream = new MemoryStream(imageBytes);
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

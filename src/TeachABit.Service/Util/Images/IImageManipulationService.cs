using Microsoft.AspNetCore.Http;

namespace TeachABit.Service.Util.Images
{
    public interface IImageManipulationService
    {
        Task<MemoryStream> ConvertToProfilePhoto(IFormFile image);
        Task<MemoryStream> ConvertToNaslovnaSlika(IFormFile image);
    }
}

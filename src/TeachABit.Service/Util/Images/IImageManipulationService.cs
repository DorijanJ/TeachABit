using Microsoft.AspNetCore.Http;

namespace TeachABit.Service.Util.Images
{
    public interface IImageManipulationService
    {
        Task<MemoryStream> ConvertToProfilePhoto(string base64Image);
        Task<MemoryStream> ConvertToNaslovnaSlika(string base64Image);
    }
}

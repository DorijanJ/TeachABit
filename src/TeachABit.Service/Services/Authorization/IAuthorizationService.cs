using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.User;

namespace TeachABit.Service.Services.Authorization
{
    public interface IAuthorizationService
    {
        ServiceResult<AppUserDto> GetUser();
    }
}

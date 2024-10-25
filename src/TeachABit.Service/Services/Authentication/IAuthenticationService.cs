using TeachABit.Model.DTOs.Authentication;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.User;

namespace TeachABit.Service.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResult<AppUserDto>> Login(LoginAttemptDTO loginAttempt);
        Task<ServiceResult<AppUserDto>> Register(RegisterAttemptDTO registerAttempt);
        ServiceResult Logout();
    }
}

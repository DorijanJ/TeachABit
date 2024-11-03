using TeachABit.Model.DTOs.Authentication;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.User;

namespace TeachABit.Service.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResult<AppUserDto>> Login(LoginAttemptDto loginAttempt);
        Task<ServiceResult<AppUserDto>> Register(RegisterAttemptDto registerAttempt);
        Task<ServiceResult<AppUserDto>> SignInGoogle(GoogleSignInAttempt googleSigninAttempt);
        Task<ServiceResult> ResetPassword(ResetPasswordDto resetPassword);
        Task<ServiceResult> ForgotPassword(ForgotPasswordDto forgotPassword);
        Task<ServiceResult> ConfirmEmail(ConfirmEmailDto confirmEmail);
        Task<ServiceResult> ResendMailConfirmationLink(ResendConfirmEmailDto resendConfirmEmail);
        ServiceResult Logout();
    }
}

using TeachABit.Model.DTOs.Authentication;
using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.DTOs.Result;

namespace TeachABit.Service.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResult> Login(LoginAttemptDto loginAttempt);
        Task<ServiceResult> Register(RegisterAttemptDto registerAttempt);
        Task<ServiceResult> SignInGoogle(GoogleSignInAttempt googleSigninAttempt);
        Task<ServiceResult> ResetPassword(ResetPasswordDto resetPassword);
        Task<ServiceResult> ForgotPassword(ForgotPasswordDto forgotPassword);
        Task<ServiceResult> ConfirmEmail(ConfirmEmailDto confirmEmail);
        Task<ServiceResult> ResendMailConfirmationLink(ResendConfirmEmailDto resendConfirmEmail);
        ServiceResult Logout();
        Task<ServiceResult<KorisnikDto>> GetKorisnikByUsername(string username);
    }
}

﻿using TeachABit.Model.DTOs.Authentication;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Korisnici;

namespace TeachABit.Service.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResult<KorisnikDto>> Login(LoginAttemptDto loginAttempt);
        Task<ServiceResult<KorisnikDto>> Register(RegisterAttemptDto registerAttempt);
        Task<ServiceResult<KorisnikDto>> SignInGoogle(GoogleSignInAttempt googleSigninAttempt);
        Task<ServiceResult> ResetPassword(ResetPasswordDto resetPassword);
        Task<ServiceResult> ForgotPassword(ForgotPasswordDto forgotPassword);
        Task<ServiceResult> ConfirmEmail(ConfirmEmailDto confirmEmail);
        Task<ServiceResult> ResendMailConfirmationLink(ResendConfirmEmailDto resendConfirmEmail);
        ServiceResult Logout();
    }
}

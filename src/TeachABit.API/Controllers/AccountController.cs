using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachABit.API.Middleware;
using TeachABit.Model.DTOs.Authentication;
using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Service.Services.Authentication;
using TeachABit.Service.Services.Korisnici;
using IAuthorizationService = TeachABit.Service.Services.Authorization.IAuthorizationService;

namespace TeachABit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAuthenticationService authenticationService, IAuthorizationService authorizationService, IKorisniciService korisniciService) : BaseController
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;
        private readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly IKorisniciService _korisniciService = korisniciService;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginAttemptDto loginAttempt)
        {
            return GetControllerResult(await _authenticationService.Login(loginAttempt));
        }

        [AllowAnonymous]
        [ModelStateFilter(MessageTypes.Hidden)]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterAttemptDto registerAttempt)
        {
            return GetControllerResult(await _authenticationService.Register(registerAttempt));
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return GetControllerResult(_authenticationService.Logout());
        }

        [AllowAnonymous]
        [HttpPost("google-signin")]
        public async Task<IActionResult> SignInGoogle(GoogleSignInAttempt googleSigninAttempt)
        {
            return GetControllerResult(await _authenticationService.SignInGoogle(googleSigninAttempt));
        }

        [AllowAnonymous]
        [ModelStateFilter(MessageTypes.Hidden)]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            return GetControllerResult(await _authenticationService.ResetPassword(resetPassword));
        }

        [AllowAnonymous]
        [ModelStateFilter(MessageTypes.Hidden)]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            return GetControllerResult(await _authenticationService.ForgotPassword(forgotPassword));
        }

        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            return Ok(_authorizationService.GetKorisnikDto());
        }

        [AllowAnonymous]
        [HttpGet("by-username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            return GetControllerResult(await _authenticationService.GetKorisnikByUsername(username));
        }

        [AllowAnonymous]
        [ModelStateFilter(MessageTypes.Hidden)]
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmEmail)
        {
            return GetControllerResult(await _authenticationService.ConfirmEmail(confirmEmail));
        }

        [AllowAnonymous]
        [ModelStateFilter(MessageTypes.Hidden)]
        [HttpPost("resend-confirm-email")]
        public async Task<IActionResult> ResendConfirmEmail(ResendConfirmEmailDto resendConfirmEmail)
        {
            return GetControllerResult(await _authenticationService.ResendMailConfirmationLink(resendConfirmEmail));
        }

        [HttpPost("update-korisnik")]
        [ModelStateFilter]
        public async Task<IActionResult> UpdateKorisnik(UpdateKorisnikDto updateKorisnik)
        {
            return GetControllerResult(await _korisniciService.UpdateKorisnik(updateKorisnik));
        }

    }
}

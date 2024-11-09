using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachABit.API.Middleware;
using TeachABit.Model.DTOs.Authentication;
using TeachABit.Service.Services.Authentication;
using IAuthorizationService = TeachABit.Service.Services.Authorization.IAuthorizationService;

namespace TeachABit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAuthenticationService authenticationService, IAuthorizationService authorizationService) : BaseController
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;
        private readonly IAuthorizationService _authorizationService = authorizationService;

        [AllowAnonymous]
        [ServiceFilter(typeof(ModelStateFilter))]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginAttemptDto loginAttempt)
        {
            return GetControllerResult(await _authenticationService.Login(loginAttempt));
        }

        [AllowAnonymous]
        [ServiceFilter(typeof(ModelStateFilter))]
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
        [ServiceFilter(typeof(ModelStateFilter))]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            return GetControllerResult(await _authenticationService.ResetPassword(resetPassword));
        }

        [AllowAnonymous]
        [ServiceFilter(typeof(ModelStateFilter))]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            return GetControllerResult(await _authenticationService.ForgotPassword(forgotPassword));
        }

        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            return GetControllerResult(_authorizationService.GetKorisnikDto());
        }

        [AllowAnonymous]
        [ServiceFilter(typeof(ModelStateFilter))]
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmEmail)
        {
            return GetControllerResult(await _authenticationService.ConfirmEmail(confirmEmail));
        }

        [AllowAnonymous]
        [ServiceFilter(typeof(ModelStateFilter))]
        [HttpPost("resend-confirm-email")]
        public async Task<IActionResult> ResendConfirmEmail(ResendConfirmEmailDto resendConfirmEmail)
        {
            return GetControllerResult(await _authenticationService.ResendMailConfirmationLink(resendConfirmEmail));
        }
    }
}

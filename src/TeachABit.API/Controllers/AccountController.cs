using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachABit.API.Middleware;
using TeachABit.Model.DTOs.Authentication;
using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.Enums;
using TeachABit.Service.Services.Authentication;
using TeachABit.Service.Services.Korisnici;
using TeachABit.Service.Services.Uloge;

namespace TeachABit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(
        IAuthenticationService authenticationService,
        IKorisniciService korisniciService,
        IUlogeService rolesService) : BaseController
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;
        private readonly IKorisniciService _korisniciService = korisniciService;
        private readonly IUlogeService _rolesService = rolesService;

        [AllowAnonymous]
        [ModelStateFilter(MessageTypes.Hidden)]
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
        [ModelStateFilter]
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
        public async Task<IActionResult> GetUsers(string? search)
        {
            return GetControllerResult(await _korisniciService.GetAllUsers(search));
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

        [HttpPost("{username}/postavi-ulogu")]
        [ModelStateFilter]
        public async Task<IActionResult> AddKorisnikToRole(string username,
            [FromBody] AddKorisnikToRoleDto addKorisnikToRole)
        {
            return GetControllerResult(await _rolesService.AddUserToUloga(username, addKorisnikToRole.RoleName));
        }

        [HttpPost("{username}/verifikacija-zahtjev")]
        [NotStatus(KorisnikStatusEnum.Utisan)]
        public async Task<IActionResult> CreateVerifikacijaZahtjev(string username)
        {
            return GetControllerResult(await _korisniciService.CreateVerifikacijaZahtjev(username));
        }

        [HttpPost("{username}/verifikacija")]
        public async Task<IActionResult> PrihvatiVerifikacijaZahtjev(string username)
        {
            return GetControllerResult(await _korisniciService.PrihvatiVerifikacijaZahtjev(username));
        }

        [HttpGet("verifikacija-zahtjev")]
        public async Task<IActionResult> GetKorisniciSaZahtjevomVerifikacije()
        {
            return GetControllerResult(await _korisniciService.GetKorisniciSaZahtjevomVerifikacije());
        }

        [HttpPost("{username}/utisaj")]
        public async Task<IActionResult> UtisajKorisnika(string username)
        {
            return GetControllerResult(await _korisniciService.UtisajKorisnika(username));
        }

        [HttpDelete("{username}/utisaj")]
        public async Task<IActionResult> OdTisajKorisnika(string username)
        {
            return GetControllerResult(await _korisniciService.OdTisajKorisnika(username));
        }

        [HttpGet("reauth")]
        public async Task<IActionResult> Reauth()
        {
            return GetControllerResult(await _authenticationService.Reauth());
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteAccount(string username)
        {
            return GetControllerResult(await _korisniciService.DeleteKorisnik(username));
        }
    }
}

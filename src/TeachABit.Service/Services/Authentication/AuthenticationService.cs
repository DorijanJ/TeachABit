using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Web;
using TeachABit.Model.DTOs.Authentication;
using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.DTOs.Uloge;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Service.Services.Authorization;
using TeachABit.Service.Util.Mail;
using TeachABit.Service.Util.Token;

namespace TeachABit.Service.Services.Authentication
{
    public class AuthenticationService(UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor, ITokenService tokenService, IMapper mapper, IMailSenderService mailSenderService, IConfiguration configuration) : IAuthenticationService
    {
        private readonly UserManager<Korisnik> _userManager = userManager;
        private readonly SignInManager<Korisnik> _signInManager = signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IMailSenderService _mailSenderService = mailSenderService;
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _configuration = configuration;
        private readonly IAuthorizationService _authorizationService = authorizationService;

        public async Task<ServiceResult> Login(LoginAttemptDto loginAttempt)
        {
            var normalizedCredentials = loginAttempt.Credentials.ToUpper();
            Korisnik? user = await _userManager.Users
                .Include(x => x.KorisnikUloge)
                .ThenInclude(x => x.Uloga)
                .FirstOrDefaultAsync(x => x.NormalizedEmail == normalizedCredentials || x.NormalizedUserName == normalizedCredentials);

            if (user == null)
            {
                return ServiceResult.Failure(MessageDescriber.UserNotFound());
            }


            SignInResult? result = await _signInManager.CheckPasswordSignInAsync(user, loginAttempt.Password, true);
            if (result.IsLockedOut)
            {
                var lockoutEnd = await _userManager.GetLockoutEndDateAsync(user);
                return ServiceResult.Failure(MessageDescriber.AccountLockedOut(lockoutEnd.Value));
            }
            if (!result.Succeeded) return ServiceResult.Failure(MessageDescriber.PasswordMismatch());

            bool useMailConfirmation = _configuration.GetValue<bool>("UseMailConfirmation");

            if (useMailConfirmation && !user.EmailConfirmed) return ServiceResult.Failure(MessageDescriber.EmailNotConfirmed());

            ServiceResult cookieSetResult = await SetAuthCookie(user);
            if (cookieSetResult.IsError) return ServiceResult.Failure(cookieSetResult.Message);

            var k = (await _userManager.Users.Include(x => x.KorisnikUloge).ThenInclude(x => x.Uloga).FirstOrDefaultAsync(x => x.Id == user.Id));
            if (k != null) user.KorisnikUloge = k.KorisnikUloge;
            RefreshUserInfoDto refresh = _mapper.Map<RefreshUserInfoDto>(user);
            refresh.IsAuthenticated = true;

            return ServiceResult.Success(refresh);
        }

        public ServiceResult Logout()
        {
            ClearAuthCookie();
            RefreshUserInfoDto refresh = new()
            {
                IsAuthenticated = false
            };
            return ServiceResult.Success(refresh: refresh);
        }

        public async Task<ServiceResult> Register(RegisterAttemptDto registerAttempt)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerAttempt.Username))
                return ServiceResult.Failure(MessageDescriber.DuplicateUsername(registerAttempt.Username));

            if (await _userManager.Users.AnyAsync(x => x.Email == registerAttempt.Email))
                return ServiceResult.Failure(MessageDescriber.DuplicateEmail(registerAttempt.Email));

            bool useMailConfirmation = _configuration.GetValue<bool>("UseMailConfirmation");

            Korisnik user = new()
            {
                Email = registerAttempt.Email,
                UserName = registerAttempt.Username,
                EmailConfirmed = !useMailConfirmation,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerAttempt.Password);
            if (!result.Succeeded && result.Errors.Any())
            {
                string errorMessage = result.Errors.First().Description;
                return ServiceResult.Failure(MessageDescriber.RegistrationError(errorMessage));
            }

            await _userManager.AddToRoleAsync(user, "Korisnik");

            if (!useMailConfirmation) return ServiceResult.Success("Račun kreiran.");

            ServiceResult mailResult = await SendEmailConfirmationMail(user);

            if (mailResult.IsError)
            {
                await _userManager.DeleteAsync(user);
                return ServiceResult.Failure(mailResult.Message);
            }

            return ServiceResult.Success(MessageDescriber.EmailConfimationSent());
        }

        private async Task<ServiceResult> SendEmailConfirmationMail(Korisnik user)
        {
            if (user.Email == null) return ServiceResult.Failure(MessageDescriber.BadRequest("Korisnik nema spremit email."));

            var url = _configuration["ClientUrl"];
            if (url == null) return ServiceResult.Failure(MessageDescriber.MissingConfiguration());

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string confirmationLink = $"{url}/confirm-email?email={user.Email}&token={Uri.EscapeDataString(token)}";

            MailMessage message = new()
            {
                Subject = "Potvrda mail adrese za TeachABit račun",
                Body = MailDescriber.EmailConfirmationMail(confirmationLink),
                IsBodyHtml = true
            };

            ServiceResult mailResult = await _mailSenderService.SendMail(message, user.Email);
            return mailResult;
        }

        private async Task<ServiceResult> SetAuthCookie(Korisnik? user = null, bool valid = true)
        {
            string? token = user == null ? "" : await _tokenService.CreateToken(user);

            var httpContext = _httpContextAccessor.HttpContext;

            if (token == null || httpContext == null) return ServiceResult.Failure(MessageDescriber.DefaultError());

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = valid ? DateTime.UtcNow.AddHours(6) : DateTime.UtcNow.AddDays(-1),
            };

            httpContext.Response.Cookies.Append("AuthToken", token, cookieOptions);
            return ServiceResult.Success();
        }

        private ServiceResult ClearAuthCookie()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null) return ServiceResult.Failure(MessageDescriber.DefaultError());

            httpContext.Response.Cookies.Delete("AuthToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
            });
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> SignInGoogle(GoogleSignInAttempt googleSigninAttempt)
        {
            GoogleJsonWebSignature.Payload payload;

            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(googleSigninAttempt.Token);
            }
            catch (InvalidJwtException)
            {
                return ServiceResult.Failure(new MessageResponse("Nevaljani GoogleIdToken.", MessageSeverities.Error));
            }

            Korisnik? user = await _userManager.FindByEmailAsync(payload.Email);

            if (user == null)
            {
                if (String.IsNullOrEmpty(googleSigninAttempt.Username)) return ServiceResult.Failure(MessageDescriber.UsernameNotProvided());

                user = new Korisnik
                {
                    Email = payload.Email,
                    UserName = googleSigninAttempt.Username,
                    EmailConfirmed = true,
                };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded && result.Errors.Any())
                {
                    string errorMessage = result.Errors.First().Description;
                    return ServiceResult.Failure(MessageDescriber.RegistrationError(errorMessage));
                }
            }

            var cookieSetResult = await SetAuthCookie(user);
            if (cookieSetResult.IsError)
            {
                return ServiceResult.Failure(cookieSetResult.Message);
            }

            var k = (await _userManager.Users.Include(x => x.KorisnikUloge).ThenInclude(x => x.Uloga).FirstOrDefaultAsync(x => x.Id == user.Id));
            if (k != null) user.KorisnikUloge = k.KorisnikUloge;
            RefreshUserInfoDto refresh = _mapper.Map<RefreshUserInfoDto>(user);
            refresh.IsAuthenticated = true;

            return ServiceResult.Success(refresh);

        }

        public async Task<ServiceResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            Korisnik? user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null) return ServiceResult.Failure(MessageDescriber.BadRequest("Loš zahtjev za oporavak lozinke."));

            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!result.Succeeded) return ServiceResult.Failure(MessageDescriber.BadRequest(result.Errors.FirstOrDefault()?.Description ?? "Loš zahtjev za oporavak lozinke."));

            return ServiceResult.Success("Lozinka uspješno promijenjena.");
        }

        public async Task<ServiceResult> ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            Korisnik? user = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (user != null && user.Email != null)
            {
                var url = _configuration["ClientUrl"];
                if (url == null) return ServiceResult.Failure(MessageDescriber.MissingConfiguration());

                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                string encodedToken = HttpUtility.UrlEncode(resetToken);
                string resetUrl = $"{url}/reset-password?token={encodedToken}&email={HttpUtility.UrlEncode(user.Email)}";

                MailMessage message = new()
                {
                    Subject = "Oporavak lozinke za TeachABit račun",
                    Body = MailDescriber.PasswordResetMail(user.UserName ?? "", resetUrl),
                    IsBodyHtml = true
                };

                ServiceResult result = await _mailSenderService.SendMail(message, user.Email);

                if (result.IsError) return result;
            }

            return ServiceResult.Success("Ako račun sa danom mail adresom postoji, poslati će se email sa poveznicom za oporavak lozinke.");
        }

        public async Task<ServiceResult> ConfirmEmail(ConfirmEmailDto confirmEmail)
        {
            Korisnik? user = await _userManager.FindByEmailAsync(confirmEmail.Email);
            if (user == null) return ServiceResult.Failure(MessageDescriber.BadRequest("Loš zahtjev za potvrdom email-a."));

            if (user.EmailConfirmed) return ServiceResult.Failure(MessageDescriber.BadRequest("Email je već potvrđen."));

            var result = await _userManager.ConfirmEmailAsync(user, confirmEmail.Token);
            if (!result.Succeeded) return ServiceResult.Failure(MessageDescriber.BadRequest(result.Errors.FirstOrDefault()?.Description ?? "Loš zahtjev za potvrdom email-a."));

            return ServiceResult.Success(MessageDescriber.EmailConfirmed());
        }

        public async Task<ServiceResult> ResendMailConfirmationLink(ResendConfirmEmailDto resendConfirmEmail)
        {
            var user = await _userManager.FindByEmailAsync(resendConfirmEmail.Email);
            if (user == null)
                return ServiceResult.Failure(MessageDescriber.BadRequest("Ako račun sa danom mail adresom postoji, poslati će se email sa poveznicom za oporavak lozinke."));

            if (user.EmailConfirmed)
                return ServiceResult.Failure(MessageDescriber.BadRequest("Email je već potvrđen."));

            var mailResult = await SendEmailConfirmationMail(user);

            if (mailResult.IsError)
            {
                return ServiceResult.Failure(mailResult.Message);
            }

            return ServiceResult.Success(MessageDescriber.EmailConfimationSent());
        }

        public async Task<ServiceResult<KorisnikDto>> GetKorisnikByUsername(string username)
        {
            var user = await _userManager.Users
                .Include(x => x.VerifikacijaStatus)
                .Include(x => x.KorisnikUloge)
                .ThenInclude(x => x.Uloga)
                .FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null) return ServiceResult.Failure(MessageDescriber.UserNotFound());

            var korisnikDto = _mapper.Map<KorisnikDto>(user);

            return ServiceResult.Success(korisnikDto);
        }

        public async Task<ServiceResult> Reauth()
        {
            var user = await _userManager.FindByIdAsync(_authorizationService.GetKorisnik().Id);

            if (user == null) return ServiceResult.Failure();

            var dbUser = await _userManager.Users.Include(x => x.KorisnikUloge).ThenInclude(x => x.Uloga).FirstOrDefaultAsync(x => x.Id == user.Id);

            if (dbUser == null) return ServiceResult.Failure();

            RefreshUserInfoDto refreshUserInfoDto = new()
            {
                Id = user.Id,
                UserName = user.UserName,
                IsAuthenticated = true,
                Roles = _mapper.Map<List<UlogaDto>>(dbUser.KorisnikUloge.Select(x => x.Uloga)),
                KorisnikStatusId = user.KorisnikStatusId,
            };

            return ServiceResult.Success(refreshUserInfoDto);
        }
    }
}

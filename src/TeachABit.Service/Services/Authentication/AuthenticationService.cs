using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeachABit.Model.DTOs.Authentication;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.DTOs.User;
using TeachABit.Model.Models;
using TeachABit.Service.Util;

namespace TeachABit.Service.Services.Authentication
{
    public class AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor, ITokenService tokenService, IMapper mapper) : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceResult<AppUserDto>> Login(LoginAttemptDTO loginAttempt)
        {
            AppUser? user = await _userManager.FindByEmailAsync(loginAttempt.Credentials)
                ?? await _userManager.FindByNameAsync(loginAttempt.Credentials);

            if (user == null)
            {
                return ServiceResult<AppUserDto>.Failure(MessageDescriber.UserNotFound());
            }

            SignInResult? result = await _signInManager.CheckPasswordSignInAsync(user, loginAttempt.Password, true);
            if (result.IsLockedOut)
            {
                var lockoutEnd = await _userManager.GetLockoutEndDateAsync(user);
                return ServiceResult<AppUserDto>.Failure(MessageDescriber.AccountLockedOut(lockoutEnd.Value));
            }
            if (!result.Succeeded) return ServiceResult<AppUserDto>.Failure(MessageDescriber.PasswordMismatch());

            ServiceResult cookieSetResult = SetAuthCookie(user);
            if (cookieSetResult.IsError) return ServiceResult<AppUserDto>.Failure(cookieSetResult.Message);

            return ServiceResult<AppUserDto>.Success(_mapper.Map<AppUserDto>(user));
        }

        public ServiceResult Logout()
        {
            ClearAuthCookie();
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<AppUserDto>> Register(RegisterAttemptDTO registerAttempt)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerAttempt.Username))
                return ServiceResult<AppUserDto>.Failure(MessageDescriber.DuplicateUsername(registerAttempt.Username));

            if (await _userManager.Users.AnyAsync(x => x.Email == registerAttempt.Email))
                return ServiceResult<AppUserDto>.Failure(MessageDescriber.DuplicateEmail(registerAttempt.Email));

            AppUser user = new()
            {
                Email = registerAttempt.Email,
                UserName = registerAttempt.Username,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerAttempt.Password);
            if (!result.Succeeded && result.Errors.Any())
            {
                string errorMessage = result.Errors.First().Description;
                return ServiceResult<AppUserDto>.Failure(MessageDescriber.RegistrationError(errorMessage));
            }

            ServiceResult cookieSetResult = SetAuthCookie(user);
            if (cookieSetResult.IsError) return ServiceResult<AppUserDto>.Failure(cookieSetResult.Message);

            return ServiceResult<AppUserDto>.Success(_mapper.Map<AppUserDto>(user));
        }

        private ServiceResult SetAuthCookie(AppUser? user = null, bool valid = true)
        {
            string? token = user == null ? "" : _tokenService.CreateToken(user);

            var httpContext = _httpContextAccessor.HttpContext;

            if (token == null || httpContext == null) return ServiceResult.Failure(MessageDescriber.DefaultError());

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = valid ? DateTime.UtcNow.AddHours(6) : DateTime.UtcNow.AddDays(-1),
            };

            httpContext.Response.Cookies.Append("AuthToken", token, cookieOptions);
            return ServiceResult.Success();
        }
        private ServiceResult ClearAuthCookie(AppUser? user = null, bool valid = true)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null) return ServiceResult.Failure(MessageDescriber.DefaultError());

            httpContext.Response.Cookies.Delete("AuthToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
            });
            return ServiceResult.Success();
        }
    }
}

﻿using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Web;
using TeachABit.Model.DTOs.Authentication;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.DTOs.User;
using TeachABit.Model.Models.User;
using TeachABit.Service.Util.Mail;
using TeachABit.Service.Util.Token;

namespace TeachABit.Service.Services.Authentication
{
    public class AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor, ITokenService tokenService, IMapper mapper, IMailSenderService mailSenderService) : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IMailSenderService _mailSenderService = mailSenderService;
        private readonly IMapper _mapper = mapper;


        public async Task<ServiceResult<AppUserDto>> Login(LoginAttemptDto loginAttempt)
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

            if (!user.EmailConfirmed) return ServiceResult<AppUserDto>.Failure(MessageDescriber.EmailNotConfirmed());

            ServiceResult cookieSetResult = SetAuthCookie(user);
            if (cookieSetResult.IsError) return ServiceResult<AppUserDto>.Failure(cookieSetResult.Message);

            return ServiceResult<AppUserDto>.Success(_mapper.Map<AppUserDto>(user));
        }

        public ServiceResult Logout()
        {
            ClearAuthCookie();
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<AppUserDto>> Register(RegisterAttemptDto registerAttempt)
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

            ServiceResult mailResult = await SendEmailConfirmationMail(user);

            if (mailResult.IsError) return ServiceResult<AppUserDto>.Failure(mailResult.Message);

            return ServiceResult<AppUserDto>.Success(_mapper.Map<AppUserDto>(user), MessageDescriber.EmailConfimationSent());
        }

        private async Task<ServiceResult> SendEmailConfirmationMail(AppUser user)
        {
            if (user.Email == null) return ServiceResult.Failure(MessageDescriber.BadRequest("User does not have an email."));

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string confirmationLink = $"https://localhost:3000/confirm-email?email={user.Email}&token={Uri.EscapeDataString(token)}";

            MailMessage message = new()
            {
                Subject = "Mail confirmation for your TeachABit account",
                Body = MailDescriber.EmailConfirmationMail(confirmationLink),
                IsBodyHtml = true
            };

            ServiceResult mailResult = await _mailSenderService.SendMail(message, user.Email);
            return mailResult;
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
                SameSite = SameSiteMode.Lax,
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
                SameSite = SameSiteMode.Lax,
            });
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<AppUserDto>> SignInGoogle(GoogleSignInAttempt googleSigninAttempt)
        {
            GoogleJsonWebSignature.Payload payload;

            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(googleSigninAttempt.Token);
            }
            catch (InvalidJwtException)
            {
                return ServiceResult<AppUserDto>.Failure(new MessageResponse("Invalid GoogleIdToken.", MessageSeverities.Error));
            }

            AppUser? user = await _userManager.FindByEmailAsync(payload.Email);

            if (user == null)
            {
                if (String.IsNullOrEmpty(googleSigninAttempt.Username)) return ServiceResult<AppUserDto>.Failure(MessageDescriber.UsernameNotProvided());

                user = new AppUser
                {
                    Email = payload.Email,
                    UserName = googleSigninAttempt.Username,
                };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded && result.Errors.Any())
                {
                    string errorMessage = result.Errors.First().Description;
                    return ServiceResult<AppUserDto>.Failure(MessageDescriber.RegistrationError(errorMessage));
                }
            }

            var cookieSetResult = SetAuthCookie(user);
            if (cookieSetResult.IsError)
            {
                return ServiceResult<AppUserDto>.Failure(cookieSetResult.Message);
            }

            return ServiceResult<AppUserDto>.Success(_mapper.Map<AppUserDto>(user), "Successfully logged in.");
        }

        public async Task<ServiceResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            AppUser? user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null) return ServiceResult.Failure(MessageDescriber.BadRequest("Invalid password reset request."));

            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!result.Succeeded) return ServiceResult.Failure(MessageDescriber.BadRequest("Invalid password reset request."));

            return ServiceResult.Success("Password has been reset.");
        }

        public async Task<ServiceResult> ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            AppUser? user = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (user != null && user.Email != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                string encodedToken = HttpUtility.UrlEncode(resetToken);
                string resetUrl = $"https://teachabit.org/reset-password?token={encodedToken}&email={HttpUtility.UrlEncode(user.Email)}";

                MailMessage message = new()
                {
                    Subject = "Password Recovery Code for Your TeachABit Account",
                    Body = MailDescriber.PasswordResetMail(user.UserName ?? "", resetUrl),
                    IsBodyHtml = true
                };

                ServiceResult result = await _mailSenderService.SendMail(message, user.Email);

                if (result.IsError) return result;
            }

            return ServiceResult.Success("If an account with that email exists, a password reset link will be sent.");
        }

        public async Task<ServiceResult> ConfirmEmail(ConfirmEmailDto confirmEmail)
        {
            AppUser? user = await _userManager.FindByEmailAsync(confirmEmail.Email);
            if (user == null) return ServiceResult.Failure(MessageDescriber.BadRequest("Invalid mail confirmation request."));

            if (user.EmailConfirmed) return ServiceResult.Failure(MessageDescriber.BadRequest("Email has already been confirmed."));

            var result = await _userManager.ConfirmEmailAsync(user, confirmEmail.Token);
            if (!result.Succeeded) return ServiceResult.Failure(MessageDescriber.BadRequest(result.Errors.FirstOrDefault()?.Description ?? "Invalid mail confirmation request."));

            return ServiceResult.Success(MessageDescriber.EmailConfirmed());
        }

        public async Task<ServiceResult> ResendMailConfirmationLink(ResendConfirmEmailDto resendConfirmEmail)
        {
            var user = await _userManager.FindByEmailAsync(resendConfirmEmail.Email);
            if (user == null)
                return ServiceResult.Failure(MessageDescriber.BadRequest("No account associated with this email."));

            if (user.EmailConfirmed)
                return ServiceResult.Failure(MessageDescriber.BadRequest("Email is already confirmed."));

            var mailResult = await SendEmailConfirmationMail(user);

            if (mailResult.IsError)
                return ServiceResult.Failure(mailResult.Message);

            return ServiceResult.Success(MessageDescriber.EmailConfimationSent());
        }
    }
}

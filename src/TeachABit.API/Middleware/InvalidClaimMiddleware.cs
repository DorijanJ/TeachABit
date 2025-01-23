using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using TeachABit.Model.DTOs.Authentication;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.DTOs.Uloge;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Service.Util.Token;

namespace TeachABit.API.Middleware
{
    public class KorisnikStatusCheckMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        private readonly ControllerResult invalidResult = new()
        {
            Message = MessageDescriber.Unauthorized(),
            RefreshUserInfo = new()
            {
                IsAuthenticated = false,
            }
        };

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                await _next(context);
                return;
            }

            using var scope = serviceProvider.CreateScope();
            var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<Korisnik>>();

            var korisnikStatusClaim = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.KorisnikStatus)?.Value;
            var korisnikRoleClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(korisnikRoleClaim))
            {
                context.Response.Cookies.Delete("AuthToken", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            var korisnik = await _userManager.FindByIdAsync(userId);
            if (korisnik == null)
            {
                context.Response.Cookies.Delete("AuthToken", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(invalidResult);
                return;
            }
            var _mapper = serviceProvider.GetRequiredService<IMapper>();

            List<UlogaDto>? korisnikUloge = _mapper.Map<List<UlogaDto>>((await _userManager.Users.Include(x => x.KorisnikUloge).ThenInclude(x => x.Uloga).FirstOrDefaultAsync(x => x.Id == korisnik.Id))?.KorisnikUloge.Select(x => x.Uloga));
            string? statusId = korisnik.KorisnikStatusId?.ToString();
            List<string> parsedRoleClaim = [.. korisnikRoleClaim.Split(",")];
            List<string> ulogaNazivi = korisnikUloge.Select(x => x.Name).ToList();

            if (korisnikStatusClaim != statusId || (parsedRoleClaim != null && !parsedRoleClaim.SequenceEqual(ulogaNazivi)))
            {
                var tokenMaker = serviceProvider.GetService<ITokenService>() ?? throw new Exception();
                var token = await tokenMaker.CreateToken(korisnik) ?? throw new Exception();

                context.Response.Cookies.Append("AuthToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(6),
                });

                context.Response.StatusCode = StatusCodes.Status200OK;

                RefreshUserInfoDto reissuedMessage = new()
                {
                    Id = korisnik.Id,
                    IsAuthenticated = true,
                    Roles = korisnikUloge,
                    UserName = korisnik.UserName,
                };
                context.Response.Headers.Add("reissued-message", JsonConvert.SerializeObject(reissuedMessage));
            }

            await _next(context);
        }

        private async Task RespondUnauthorizedAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(invalidResult);
        }
    }
}

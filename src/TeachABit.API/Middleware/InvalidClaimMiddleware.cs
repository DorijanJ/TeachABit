using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                await _next(context);
                return;
            }

            using var scope = serviceProvider.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Korisnik>>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();

            var korisnik = await GetAuthenticatedUserAsync(context, userManager);
            if (korisnik == null)
            {
                await RespondUnauthorizedAsync(context);
                return;
            }

            var korisnikUloge = await GetUserRolesAsync(korisnik, userManager, mapper);
            if (IsUserInfoInvalid(context, korisnik, korisnikUloge))
            {
                await ReissueTokenAsync(context, tokenService, korisnik, korisnikUloge);
                return;
            }

            await _next(context);
        }

        private static async Task<Korisnik?> GetAuthenticatedUserAsync(HttpContext context, UserManager<Korisnik> userManager)
        {
            string userId = context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value!;
            return string.IsNullOrEmpty(userId) ? null : await userManager.FindByIdAsync(userId);
        }

        private static async Task<List<UlogaDto>> GetUserRolesAsync(Korisnik korisnik, UserManager<Korisnik> userManager, IMapper mapper)
        {
            var userWithRoles = await userManager.Users
                .Include(u => u.KorisnikUloge)
                .ThenInclude(ku => ku.Uloga)
                .FirstOrDefaultAsync(u => u.Id == korisnik.Id);

            return mapper.Map<List<UlogaDto>>(userWithRoles?.KorisnikUloge.Select(ku => ku.Uloga));
        }

        private static bool IsUserInfoInvalid(HttpContext context, Korisnik korisnik, List<UlogaDto> korisnikUloge)
        {
            var korisnikStatusClaim = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.KorisnikStatus)?.Value;
            var korisnikRolesClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            string? currentStatusId = korisnik.KorisnikStatusId?.ToString();
            var parsedRolesFromClaim = korisnikRolesClaim?.Split(",") ?? Array.Empty<string>();
            var ulogaNazivi = korisnikUloge.Select(u => u.Name).ToArray();

            return korisnikStatusClaim != currentStatusId || !parsedRolesFromClaim.SequenceEqual(ulogaNazivi);
        }

        private static async Task ReissueTokenAsync(HttpContext context, ITokenService tokenService, Korisnik korisnik, List<UlogaDto> korisnikUloge)
        {
            var token = await tokenService.CreateToken(korisnik);

            context.Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(6),
            });

            var refreshUserInfo = new ControllerResult()
            {
                RefreshUserInfo = new()
                {
                    Id = korisnik.Id,
                    IsAuthenticated = true,
                    Roles = korisnikUloge,
                    UserName = korisnik.UserName,
                    KorisnikStatusId = korisnik.KorisnikStatusId,
                },
                Message = MessageDescriber.Reauthenticate()
            };

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(refreshUserInfo);
        }

        private static async Task RespondUnauthorizedAsync(HttpContext context)
        {
            var invalidResult = new ControllerResult
            {
                Message = MessageDescriber.Unauthenticated(),
                RefreshUserInfo = new RefreshUserInfoDto
                {
                    IsAuthenticated = false,
                },
            };

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(invalidResult);
        }
    }
}

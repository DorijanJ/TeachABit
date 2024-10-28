using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.User;

namespace TeachABit.Service.Services.Authorization
{
    public class AuthorizationService(IHttpContextAccessor httpContextAccessor) : IAuthorizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public ServiceResult<AppUserDto> GetUser()
        {
            var httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("No HTTP context available.");
            var token = httpContext.Request.Cookies["AuthToken"];

            if (token == null) throw new UnauthorizedAccessException();

            var jwtHandler = new JwtSecurityTokenHandler();
            var jwt = jwtHandler.ReadJwtToken(token);
            var claim = jwt.Claims.FirstOrDefault(claim => claim.Type == "unique_name");
            return claim == null ? throw new UnauthorizedAccessException() : ServiceResult<AppUserDto>.Success(new AppUserDto()
            {
                UserName = claim.Value,
            });
        }
    }
}

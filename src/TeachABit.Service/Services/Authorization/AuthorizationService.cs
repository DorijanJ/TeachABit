using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Service.Services.Authorization
{
    public class AuthorizationService(IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<Korisnik> userManager) : IAuthorizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<Korisnik> _userManager = userManager;

        public Korisnik GetKorisnik()
        {
            var httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("No HTTP context available.");
            var token = httpContext.Request.Cookies["AuthToken"] ?? throw new UnauthorizedAccessException();
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwt = jwtHandler.ReadJwtToken(token);
            var usernameClaim = jwt.Claims.FirstOrDefault(claim => claim.Type == "unique_name");
            var idClaim = jwt.Claims.FirstOrDefault(claim => claim.Type == "nameid");
            var emailClaim = jwt.Claims.FirstOrDefault(claim => claim.Type == "email");
            return (usernameClaim == null || idClaim == null || emailClaim == null) ? throw new UnauthorizedAccessException() : new Korisnik()
            {
                Email = emailClaim.Value,
                UserName = usernameClaim.Value,
                Id = idClaim.Value,
            };
        }

        public KorisnikDto GetKorisnikDto()
        {
            return _mapper.Map<KorisnikDto>(GetKorisnik());
        }

        public async Task<KorisnikDto> GetKorisnikFull()
        {
            var korisnik = GetKorisnik();
            return _mapper.Map<KorisnikDto>(await _userManager.FindByIdAsync(korisnik.Id));
        }
    }
}

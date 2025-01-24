using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Service.Util.Token
{
    public class TokenService(IConfiguration configuration, UserManager<Korisnik> userManager) : ITokenService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly UserManager<Korisnik> _userManager = userManager;

        public async Task<string> CreateToken(Korisnik user)
        {
            if (user.UserName == null || user.Email == null) throw new Exception("Greška pri stvaranju tokena.");

            var roles = await _userManager.GetRolesAsync(user);
            List<Claim> roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email),
            };

            claims.AddRange(roleClaims);

            if (user.KorisnikStatusId.HasValue)
            {
                claims.Add(new(CustomClaimTypes.KorisnikStatus, user.KorisnikStatusId.Value.ToString()));
            }

            var secretKey = _configuration["JwtSettings:Key"] ?? throw new ConfigurationErrorsException();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.Enums;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Uloge;

namespace TeachABit.Service.Services.Authorization
{
    public class AuthorizationService(IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<Korisnik> userManager, RoleManager<Uloga> roleManager) : IAuthorizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<Korisnik> _userManager = userManager;
        private readonly RoleManager<Uloga> _roleManager = roleManager;

        public Korisnik GetKorisnik()
        {
            var korisnik = GetKorisnikOptional();
            return korisnik ?? throw new UnauthorizedAccessException();
        }

        public Korisnik? GetKorisnikOptional()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return null;
            var token = httpContext.Request.Cookies["AuthToken"];
            if (token == null) return null;
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwt = jwtHandler.ReadJwtToken(token);
            var usernameClaim = jwt.Claims.FirstOrDefault(claim => claim.Type == "unique_name");
            var idClaim = jwt.Claims.FirstOrDefault(claim => claim.Type == "nameid");
            var emailClaim = jwt.Claims.FirstOrDefault(claim => claim.Type == "email");
            return (usernameClaim == null || idClaim == null || emailClaim == null) ? null : new Korisnik()
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

        public async Task<Korisnik> GetKorisnikFull()
        {
            var korisnik = GetKorisnik();
            var fullKorisnik = await _userManager.Users
                .Include(x => x.KorisnikUloge)
                .ThenInclude(x => x.Uloga)
                .Include(x => x.VerifikacijaStatus)
                .FirstOrDefaultAsync(k => k.Id == korisnik.Id);
            return fullKorisnik ?? throw new UnauthorizedAccessException();
        }

        public async Task<bool> HasPermission(LevelPristupaEnum levelPristupa)
        {
            var korisnik = GetKorisnik();

            var userRoles = await _userManager.GetRolesAsync(korisnik);
            var highestRole = _roleManager.Roles
                .Where(role => role.Name != null && userRoles.Contains(role.Name))
                .Select(role => role.LevelPristupa)
                .Max();
            return highestRole >= (int)levelPristupa;
        }
    }
}

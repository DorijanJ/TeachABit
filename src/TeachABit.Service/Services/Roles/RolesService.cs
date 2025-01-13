using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Service.Services.Uloge
{
    public class RolesService(UserManager<Korisnik> userManager, RoleManager<IdentityRole> roleManager) : IRolesService
    {
        private readonly UserManager<Korisnik> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        public async Task<ServiceResult> AddUserToRole(string userName, string roleName)
        {
            var korisnik = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (korisnik == null) return ServiceResult.Failure(MessageDescriber.BadRequest("Korisnik ne postoji."));

            var uloga = await _roleManager.FindByNameAsync(roleName);
            if (uloga == null) return ServiceResult.Failure(MessageDescriber.BadRequest("Uloga ne postoji."));

            await _userManager.AddToRoleAsync(korisnik, roleName);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RemoveUserFromRole(string userName, string roleName)
        {
            var korisnik = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (korisnik == null) return ServiceResult.Failure(MessageDescriber.BadRequest("Korisnik ne postoji."));

            var uloga = await _roleManager.FindByNameAsync(roleName);
            if (uloga == null) return ServiceResult.Failure(MessageDescriber.BadRequest("Uloga ne postoji."));

            var ulogeKorisnika = await _userManager.GetRolesAsync(korisnik);

            if (ulogeKorisnika.Any(x => x.Equals(roleName, StringComparison.OrdinalIgnoreCase)))
                return ServiceResult.Failure(MessageDescriber.BadRequest($"Korisnik nije {roleName}."));

            await _userManager.RemoveFromRoleAsync(korisnik, roleName);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<string?>>> GetAllRoles()
        {
            List<string?> roles = await _roleManager.Roles.Select(x => x.Name).ToListAsync();
            return ServiceResult.Success(roles.Where(x => !string.IsNullOrEmpty(x)).ToList());
        }
    }
}

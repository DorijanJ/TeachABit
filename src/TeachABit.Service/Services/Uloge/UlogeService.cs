using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.DTOs.Uloge;
using TeachABit.Model.Enums;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Uloge;

namespace TeachABit.Service.Services.Uloge
{
    public class UlogeService(UserManager<Korisnik> userManager, RoleManager<Uloga> roleManager, IMapper mapper) : IUlogeService
    {
        private readonly UserManager<Korisnik> _userManager = userManager;
        private readonly RoleManager<Uloga> _roleManager = roleManager;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceResult> AddUserToUloga(string userName, string roleName)
        {
            var korisnik = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (korisnik == null) return ServiceResult.Failure(MessageDescriber.BadRequest("Korisnik ne postoji."));

            var uloga = await _roleManager.FindByNameAsync(roleName);
            if (uloga == null) return ServiceResult.Failure(MessageDescriber.BadRequest("Uloga ne postoji."));

            var roles = await _userManager.GetRolesAsync(korisnik);
            if (roles.Any(x => x == "Admin")) return ServiceResult.Failure(MessageDescriber.Unauthorized());
            await _userManager.RemoveFromRolesAsync(korisnik, roles);

            if(roleName == "Moderator")
            {
                korisnik.VerifikacijaStatusId = (int)VerifikacijaEnum.Verificiran;
            }

            await _userManager.AddToRoleAsync(korisnik, roleName);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<UlogaDto>>> GetAllUloge()
        {
            List<Uloga> roles = await _roleManager.Roles.Where(x => x.LevelPristupa < 3).ToListAsync();
            return ServiceResult.Success(_mapper.Map<List<UlogaDto>>(roles));
        }
    }
}

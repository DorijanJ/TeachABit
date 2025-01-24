using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.Enums;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Service.Services.Authentication;
using TeachABit.Service.Services.Authorization;
using TeachABit.Service.Util.Images;
using TeachABit.Service.Util.S3;

namespace TeachABit.Service.Services.Korisnici
{
    public class KorisniciService(IAuthorizationService authorizationService, IAuthenticationService authenticationService, IS3BucketService s3BucketService, UserManager<Korisnik> userManager, IMapper mapper, IImageManipulationService imageManipulationService) : IKorisniciService
    {
        private readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly IS3BucketService _s3BucketService = s3BucketService;
        private readonly UserManager<Korisnik> _userManager = userManager;
        private readonly IImageManipulationService _imageManipulationService = imageManipulationService;
        private readonly IAuthenticationService _authenticationService = authenticationService;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceResult<KorisnikDto>> CreateVerifikacijaZahtjev(string username)
        {
            var korisnik = await _authorizationService.GetKorisnikFull();

            if (korisnik.UserName != username) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            if (korisnik.VerifikacijaStatusId != null) return ServiceResult.Failure(MessageDescriber.BadRequest("Već ste slali zahtjev."));

            korisnik.VerifikacijaStatusId = (int)VerifikacijaEnum.ZahtjevPoslan;
            await _userManager.UpdateAsync(korisnik);

            return ServiceResult.Success(_mapper.Map<KorisnikDto>(korisnik));
        }

        public async Task<ServiceResult<List<KorisnikDto>>> GetAllUsers(string? search)
        {
            var korisnik = _authorizationService.GetKorisnik();
            var authorized = await _authorizationService.HasPermission(LevelPristupaEnum.Moderator);

            if (!authorized) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            var korisnici = _userManager.Users
                .Include(x => x.KorisnikUloge)
                .ThenInclude(x => x.Uloga)
                .Include(x => x.KorisnikStatus)
                .Include(x => x.VerifikacijaStatus).AsQueryable();

            if (!string.IsNullOrEmpty(search)) korisnici = korisnici.Where(k => k.NormalizedUserName == search.ToUpper());

            var korisnikList = await korisnici.ToListAsync();

            return ServiceResult.Success(_mapper.Map<List<KorisnikDto>>(korisnikList));
        }

        public async Task<ServiceResult<List<KorisnikDto>>> GetKorisniciSaZahtjevomVerifikacije()
        {
            var korisnik = _authorizationService.GetKorisnik();
            var authorized = await _userManager.IsInRoleAsync(korisnik, "Moderator") || await _userManager.IsInRoleAsync(korisnik, "Admin");

            if (!authorized) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            var korisnici = await _userManager.Users.Include(x => x.VerifikacijaStatus).Where(x => x.VerifikacijaStatusId == (int)VerifikacijaEnum.ZahtjevPoslan).ToListAsync();

            return ServiceResult.Success(_mapper.Map<List<KorisnikDto>>(korisnici));
        }

        public async Task<ServiceResult<KorisnikDto>> PrihvatiVerifikacijaZahtjev(string username)
        {
            var korisnik = _authorizationService.GetKorisnik();

            var authorized = await _userManager.IsInRoleAsync(korisnik, "Moderator") || await _userManager.IsInRoleAsync(korisnik, "Admin");

            if (!authorized) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            var korisnikZaVerifikaciju = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (korisnikZaVerifikaciju == null) return ServiceResult.Failure(MessageDescriber.BadRequest("Korisnik nije pronađen."));
            if (korisnikZaVerifikaciju.VerifikacijaStatusId != (int)VerifikacijaEnum.ZahtjevPoslan) return ServiceResult.Failure(MessageDescriber.BadRequest("Korisnik nije predao zahtjev."));

            korisnikZaVerifikaciju.VerifikacijaStatusId = (int)VerifikacijaEnum.Verificiran;
            await _userManager.UpdateAsync(korisnikZaVerifikaciju);

            return ServiceResult.Success(_mapper.Map<KorisnikDto>(korisnikZaVerifikaciju));
        }

        public async Task<ServiceResult<KorisnikDto>> UpdateKorisnik(UpdateKorisnikDto updateKorisnik)
        {
            try
            {
                var korisnikId = _authorizationService.GetKorisnik().Id;
                var korisnik = await _userManager.FindByIdAsync(korisnikId);

                if (korisnik == null || (!string.IsNullOrEmpty(updateKorisnik.Username) && korisnik.UserName != updateKorisnik.Username)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

                if (korisnik != null && updateKorisnik.ProfilnaSlikaBase64 != null)
                {
                    string profilnaSlikaVersion = Guid.NewGuid().ToString();
                    var resizedSlika = await _imageManipulationService.ConvertToProfilePhoto(updateKorisnik.ProfilnaSlikaBase64);
                    await _s3BucketService.UploadImageAsync(korisnikId, resizedSlika);
                    korisnik.ProfilnaSlikaVersion = profilnaSlikaVersion;
                    await _userManager.UpdateAsync(korisnik);
                    return ServiceResult.Success(_mapper.Map<KorisnikDto>(korisnik));
                }
                return ServiceResult.Success(_mapper.Map<KorisnikDto>(korisnik));
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure(MessageDescriber.DefaultError(ex.Message));
            }
        }

        public async Task<ServiceResult> UtisajKorisnika(string username)
        {
            if (!(await _authorizationService.HasPermission(LevelPristupaEnum.Moderator))) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            var korisnik = await _userManager.FindByNameAsync(username);
            if (korisnik == null) return ServiceResult.Failure(MessageDescriber.BadRequest("Korisnik nije pronađen"));

            var roles = await _userManager.GetRolesAsync(korisnik);
            if (roles.Any(x => x == "Admin" || x == "Moderator")) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            if (korisnik.KorisnikStatusId == (int)KorisnikStatusEnum.Utisan) return ServiceResult.Failure(MessageDescriber.BadRequest("Korisnik već utišan."));

            korisnik.KorisnikStatusId = (int)KorisnikStatusEnum.Utisan;
            await _userManager.UpdateAsync(korisnik);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> OdTisajKorisnika(string username)
        {
            if (!(await _authorizationService.HasPermission(LevelPristupaEnum.Moderator))) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            var korisnik = await _userManager.FindByNameAsync(username);
            if (korisnik == null) return ServiceResult.Failure(MessageDescriber.BadRequest("Korisnik nije pronađen"));

            var roles = await _userManager.GetRolesAsync(korisnik);
            if (roles.Any(x => x == "Admin" || x == "Moderator")) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            if (korisnik.KorisnikStatusId != (int)KorisnikStatusEnum.Utisan) return ServiceResult.Failure(MessageDescriber.BadRequest("Korisnik nije utišan."));

            korisnik.KorisnikStatusId = null;
            await _userManager.UpdateAsync(korisnik);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteKorisnik(string username)
        {
            var korisnikId = _authorizationService.GetKorisnik().Id;
            var user = await _userManager.FindByIdAsync(korisnikId);

            if (user == null) return ServiceResult.Failure();

            if (user.NormalizedUserName != username.ToUpper())
            {
                var userForDelete = await _userManager.FindByNameAsync(username);
                if (userForDelete == null) return ServiceResult.Failure(MessageDescriber.ItemNotFound());
                if (!(await _authorizationService.HasPermission(LevelPristupaEnum.Admin))) return ServiceResult.Failure(MessageDescriber.Unauthorized());
                if (await _userManager.IsInRoleAsync(userForDelete, "Admin")) return ServiceResult.Failure(MessageDescriber.Unauthorized());
                await _userManager.DeleteAsync(userForDelete);
                return ServiceResult.Success();
            }

            await _userManager.DeleteAsync(user);
            return _authenticationService.Logout();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Service.Services.Authorization;
using TeachABit.Service.Util.Images;
using TeachABit.Service.Util.S3;

namespace TeachABit.Service.Services.Korisnici
{
    public class KorisniciService(IAuthorizationService authorizationService, IS3BucketService s3BucketService, UserManager<Korisnik> userManager, IMapper mapper, IImageManipulationService imageManipulationService) : IKorisniciService
    {
        private readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly IS3BucketService _s3BucketService = s3BucketService;
        private readonly UserManager<Korisnik> _userManager = userManager;
        private readonly IImageManipulationService _imageManipulationService = imageManipulationService;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceResult<KorisnikDto>> UpdateKorisnik(UpdateKorisnikDto updateKorisnik)
        {
            try
            {
                var korisnikId = _authorizationService.GetKorisnik().Id;
                var korisnik = await _userManager.FindByIdAsync(korisnikId);

                if (korisnik != null && updateKorisnik.ProfilnaSlika != null)
                {
                    string profilnaSlikaVersion = Guid.NewGuid().ToString();
                    var resizedSlika = await _imageManipulationService.ConvertToProfilePhoto(updateKorisnik.ProfilnaSlika);
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
    }
}

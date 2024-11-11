using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Service.Services.Authorization
{
    public interface IAuthorizationService
    {
        Korisnik GetKorisnik();
        ServiceResult<KorisnikDto> GetKorisnikDto();
    }
}

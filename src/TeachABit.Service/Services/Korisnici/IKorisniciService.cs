using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.DTOs.Result;

namespace TeachABit.Service.Services.Korisnici
{
    public interface IKorisniciService
    {
        Task<ServiceResult<KorisnikDto>> UpdateKorisnik(UpdateKorisnikDto updateKorisnik);
    }
}

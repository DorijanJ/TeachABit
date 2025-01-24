using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.DTOs.Result;

namespace TeachABit.Service.Services.Korisnici
{
    public interface IKorisniciService
    {
        Task<ServiceResult<KorisnikDto>> UpdateKorisnik(UpdateKorisnikDto updateKorisnik);
        Task<ServiceResult<KorisnikDto>> CreateVerifikacijaZahtjev(string username);
        Task<ServiceResult<List<KorisnikDto>>> GetKorisniciSaZahtjevomVerifikacije();
        Task<ServiceResult<KorisnikDto>> PrihvatiVerifikacijaZahtjev(string username);
        Task<ServiceResult<List<KorisnikDto>>> GetAllUsers(string? search);
        Task<ServiceResult> UtisajKorisnika(string username);
        Task<ServiceResult> OdTisajKorisnika(string username);
        Task<ServiceResult> DeleteKorisnik();
    }
}

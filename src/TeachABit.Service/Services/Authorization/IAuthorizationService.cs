using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Service.Services.Authorization
{
    public interface IAuthorizationService
    {
        Korisnik GetKorisnik();
        KorisnikDto GetKorisnikDto();
        Task<Korisnik> GetKorisnikFull();
        Korisnik? GetKorisnikOptional();
        Task<bool> IsAdmin();
    }
}

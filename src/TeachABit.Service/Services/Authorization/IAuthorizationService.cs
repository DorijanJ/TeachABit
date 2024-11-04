using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Service.Services.Authorization
{
    public interface IAuthorizationService
    {
        Korisnik GetKorisnik();
    }
}

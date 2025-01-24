using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Service.Util.Token
{
    public interface ITokenService
    {
        Task<string> CreateToken(Korisnik user);
    }
}

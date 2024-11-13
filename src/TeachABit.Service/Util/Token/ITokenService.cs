using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Service.Util.Token
{
    public interface ITokenService
    {
        string? CreateToken(Korisnik user);
    }
}

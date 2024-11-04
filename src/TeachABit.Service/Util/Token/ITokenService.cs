using TeachABit.Model.Models.User;

namespace TeachABit.Service.Util.Token
{
    public interface ITokenService
    {
        string? CreateToken(Korisnik user);
    }
}

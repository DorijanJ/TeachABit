using TeachABit.Model.Models;

namespace TeachABit.Service.Util
{
    public interface ITokenService
    {
        string? CreateToken(AppUser user);
    }
}

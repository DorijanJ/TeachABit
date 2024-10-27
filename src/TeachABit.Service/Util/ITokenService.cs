using TeachABit.Model.Models.User;

namespace TeachABit.Service.Util
{
    public interface ITokenService
    {
        string? CreateToken(AppUser user);
    }
}

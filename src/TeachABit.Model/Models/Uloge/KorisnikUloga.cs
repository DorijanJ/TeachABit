using Microsoft.AspNetCore.Identity;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Uloge
{
    public class KorisnikUloga : IdentityUserRole<string>
    {
        public override string UserId { get; set; } = string.Empty;
        public virtual Korisnik Korisnik { get; set; } = null!;
        public override string RoleId { get; set; } = string.Empty;
        public virtual Uloga Uloga { get; set; } = null!;
    }
}

using Microsoft.AspNetCore.Identity;
using TeachABit.Model.Models.Forumi;

namespace TeachABit.Model.Models.User
{
    public class Korisnik : IdentityUser
    {
        public IEnumerable<Objava> Objave { get; set; } = [];
    }
}

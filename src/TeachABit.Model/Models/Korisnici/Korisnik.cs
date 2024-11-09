using Microsoft.AspNetCore.Identity;
using TeachABit.Model.Models.Objave;

namespace TeachABit.Model.Models.Korisnici
{
    public class Korisnik : IdentityUser
    {
        public virtual List<Objava> Objave { get; set; } = [];
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Uloge
{
    public class KorisnikUloga : IdentityUserRole<string>
    {
        public virtual Korisnik Korisnik { get; set; } = null!;
        public virtual Uloga Uloga { get; set; } = null!;
    }
}

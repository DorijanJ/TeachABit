using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Objave;

namespace TeachABit.Model.Models.Korisnici
{
    [Table("AspNetUsers")]
    public class Korisnik : IdentityUser
    {
        public bool Verificiran { get; set; } = false;
        public virtual List<ObjavaReakcija> ObjavaReakcijaList { get; set; } = [];
        public virtual List<Objava> Objave { get; set; } = [];
        public string? ProfilnaSlikaVersion { get; set; } = string.Empty;
        public virtual List<KomentarReakcija> KomentarReakcijaList { get; set; } = [];
    }
}

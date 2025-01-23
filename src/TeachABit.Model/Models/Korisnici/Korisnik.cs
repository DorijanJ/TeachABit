using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Objave;
using TeachABit.Model.Models.Uloge;

namespace TeachABit.Model.Models.Korisnici
{
    [Table("AspNetUsers")]
    public class Korisnik : IdentityUser
    {
        public int? VerifikacijaStatusId { get; set; } = null;
        [ForeignKey(nameof(VerifikacijaStatusId))]
        public virtual VerifikacijaStatus? VerifikacijaStatus { get; set; } = null;

        public virtual List<ObjavaReakcija> ObjavaReakcijaList { get; set; } = [];
        public virtual List<Objava> Objave { get; set; } = [];
        public virtual List<KorisnikUloga> KorisnikUloge { get; set; } = [];
        public string? ProfilnaSlikaVersion { get; set; } = string.Empty;
        public virtual List<ObjavaKomentarReakcija> KomentarReakcijaList { get; set; } = [];

        public int? KorisnikStatusId { get; set; } = null;
        [ForeignKey(nameof(KorisnikStatusId))]
        public virtual KorisnikStatus? KorisnikStatus { get; set; } = null;
    }
}

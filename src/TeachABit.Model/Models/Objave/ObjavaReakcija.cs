using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Objave
{
    [Table("ObjavaReakcija")]
    public class ObjavaReakcija
    {
        [Key]
        public int Id { get; set; }
        public string KorisnikId { get; set; } = string.Empty;
        [ForeignKey(nameof(KorisnikId))]
        public virtual Korisnik Korisnik { get; set; } = null!;
        public int ObjavaId { get; set; }
        [ForeignKey(nameof(ObjavaId))]
        public virtual Objava Objava { get; set; } = null!;
        public bool Liked { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Objave
{
    [Table("Objava")]
    public class Objava
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public string Sadrzaj { get; set; } = string.Empty;
        public string VlasnikId { get; set; } = string.Empty;
        [ForeignKey(nameof(VlasnikId))]
        public required virtual Korisnik Vlasnik { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

        public virtual List<Komentar> Komentari { get; set; } = [];
        public virtual List<ObjavaReakcija> ObjavaReakcijaList { get; set; } = [];
    }
}

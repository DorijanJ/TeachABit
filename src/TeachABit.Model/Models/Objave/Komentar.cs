using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Objave
{
    [Table("Komentar")]
    public class Komentar
    {
        [Key]
        public int Id { get; set; }
        public string Sadrzaj { get; set; } = string.Empty;

        public required string VlasnikId { get; set; }
        [ForeignKey(nameof(VlasnikId))]
        public required virtual Korisnik Vlasnik { get; set; }

        public required int ObjavaId { get; set; }
        [ForeignKey(nameof(ObjavaId))]
        public required virtual Objava Objava { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}

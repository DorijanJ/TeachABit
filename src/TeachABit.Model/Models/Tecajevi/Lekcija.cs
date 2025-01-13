using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Objave;

namespace TeachABit.Model.Models.Tecajevi
{
    [Table("Lekcija")]
    public class Lekcija
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Sadrzaj { get; set; } = string.Empty;

        public required string VlasnikId { get; set; }
        [ForeignKey(nameof(VlasnikId))]
        public required virtual Korisnik Vlasnik { get; set; }

        public required int TecajId { get; set; }
        [ForeignKey(nameof(TecajId))]
        public required virtual Tecaj Tecaj { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Tecajevi
{
    [Table("Tecaj")]
    public class Tecaj
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public string Sadrzaj { get; set; } = string.Empty;

        public required string VlasnikId { get; set; } = string.Empty;
        [ForeignKey(nameof(VlasnikId))]
        public required virtual Korisnik Vlasnik { get; set; }

        public virtual List<Lekcija> Lekcije { get; set; } = [];
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Tecajevi
{
    [Table("TecajPlacanje")]
    public class TecajPlacanje
    {
        [Key]
        public int Id { get; set; }

        public required string KorisnikId { get; set; }
        [ForeignKey(nameof(KorisnikId))]
        public virtual Korisnik Korisnik { get; set; } = null!;

        public required int TecajId { get; set; }
        [ForeignKey(nameof(TecajId))]
        public virtual Tecaj Tecaj { get; set; } = null!;

        public decimal PoCijeni;
        public DateTime VrijemePlacanja;
    }
}

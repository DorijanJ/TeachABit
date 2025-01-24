using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Tecajevi;

namespace TeachABit.Model.Models.Radionice;
[Table("RadionicaPlacanje")]
public class RadionicaPlacanje
{ 
        [Key]
        public int Id { get; set; }

        public required string KorisnikId { get; set; }
        [ForeignKey(nameof(KorisnikId))]
        public virtual Korisnik Korisnik { get; set; } = null!;

        public required int RadionicaId { get; set; }
        [ForeignKey(nameof(RadionicaId))]
        public virtual Radionica Radionica { get; set; } = null!;

        public decimal PoCijeni;
        public DateTime VrijemePlacanja;
}
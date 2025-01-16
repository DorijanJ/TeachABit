using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
        [AllowNull]
        public decimal? Cijena { get; set; } = null;
        public bool isPublished{ get; set; }=false;

        public required string VlasnikId { get; set; } = string.Empty;
        [ForeignKey(nameof(VlasnikId))]
        public required virtual Korisnik Vlasnik { get; set; }
        public string Opis { get; set; } = string.Empty;

        public virtual List<Lekcija> Lekcije { get; set; } = [];
        public virtual List<TecajPlacanje> TecajPlacanja { get; set; } = [];
    }
}
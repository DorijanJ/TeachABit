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
        public string Opis { get; set; } = string.Empty;
        [AllowNull]
        public decimal? Cijena { get; set; } = null;
        public bool IsPublished { get; set; } = false;

        public required string VlasnikId { get; set; } = string.Empty;
        [ForeignKey(nameof(VlasnikId))]
        public required virtual Korisnik Vlasnik { get; set; }
        public string? NaslovnaSlikaVersion { get; set; } = null;

        public virtual List<Lekcija> Lekcije { get; set; } = [];
        public virtual List<TecajPlacanje> TecajPlacanja { get; set; } = [];
        public virtual List<TecajKomentar> Komentari { get; set; } = [];
        public virtual List<KorisnikTecajOcjena> KorisniciOcjena { get; set; } = [];
    }

}
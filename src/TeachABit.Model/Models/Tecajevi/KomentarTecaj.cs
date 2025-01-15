using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Tecajevi
{
    [Table("KomentarTecaj")]
    public class KomentarTecaj
    {
        [Key] 
        public int Id { get; set; }
        public string Sadrzaj { get; set; } = string.Empty;

        public required string VlasnikId { get; set; }
        [ForeignKey(nameof(VlasnikId))] public required virtual Korisnik Vlasnik { get; set; }

        public required int TecajId { get; set; }
        [ForeignKey(nameof(TecajId))] public required virtual Tecaj Tecaj { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; } = null;

        public bool IsDeleted { get; set; } = false;

        public int? NadKomentarId { get; set; } = null;
        [ForeignKey(nameof(NadKomentarId))] public virtual KomentarTecaj? NadKomentar { get; set; }

        public virtual List<KomentarTecaj> PodKomentarList { get; set; } = [];
        public virtual List<KomentarTecajReakcija> KomentarReakcijaList { get; set; } = [];
    }
}
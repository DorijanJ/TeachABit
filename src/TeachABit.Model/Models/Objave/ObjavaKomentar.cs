using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Objave
{
    [Table("Komentar")]
    public class ObjavaKomentar
    {
        [Key]
        public int Id { get; set; }
        public string Sadrzaj { get; set; } = string.Empty;

        public required string VlasnikId { get; set; }
        [ForeignKey(nameof(VlasnikId))]
        public required virtual Korisnik Vlasnik { get; set; }

        public bool? OznacenTocan { get; set; } = null;
        public required int ObjavaId { get; set; }
        [ForeignKey(nameof(ObjavaId))]
        public required virtual Objava Objava { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; } = null;

        public bool IsDeleted { get; set; } = false;

        public int? NadKomentarId { get; set; } = null;
        [ForeignKey(nameof(NadKomentarId))]
        public virtual ObjavaKomentar? NadKomentar { get; set; }

        public virtual List<ObjavaKomentar> PodKomentarList { get; set; } = [];
        public virtual List<ObjavaKomentarReakcija> KomentarReakcijaList { get; set; } = [];
    }
}

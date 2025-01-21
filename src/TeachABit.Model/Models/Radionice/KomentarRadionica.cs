using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Radionice;

[Table("KomentarRadionica")]
public class KomentarRadionica
{
    [Key]
    public int Id { get; set; }
    public string Sadrzaj { get; set; } = string.Empty;

    public required string VlasnikId { get; set; }
    [ForeignKey(nameof(VlasnikId))]
    public required virtual Korisnik Vlasnik { get; set; }

    public required int RadionicaId { get; set; }
    [ForeignKey(nameof(RadionicaId))]
    public required virtual Radionica Radionica { get; set; }

    public DateTime CreatedDateTime { get; set; }
    public DateTime? LastUpdatedDateTime { get; set; } = null;

    public bool IsDeleted { get; set; } = false;

    public int? NadKomentarId { get; set; } = null;
    [ForeignKey(nameof(NadKomentarId))]
    public virtual KomentarRadionica? NadKomentar { get; set; }

    public virtual List<KomentarRadionica> PodKomentarList { get; set; } = [];
    public virtual List<KomentarRadionicaReakcija> KomentarRadionicaReakcijaList { get; set; } = [];
}
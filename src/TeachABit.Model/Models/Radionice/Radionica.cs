using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Radionice;

[Table("Radionica")]
public class Radionica
{
    [Key]
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public string Opis { get; set; } = string.Empty;
    public decimal? Cijena { get; set; } = null;
    public DateTime VrijemeRadionice { get; set; }
    public int MaksimalniKapacitet { get; set; }

    public required string VlasnikId { get; set; } = string.Empty;
    [ForeignKey(nameof(VlasnikId))]
    public required virtual Korisnik Vlasnik { get; set; }
    
    public virtual List<KomentarRadionica> Komentari { get; set; } = [];
    
}
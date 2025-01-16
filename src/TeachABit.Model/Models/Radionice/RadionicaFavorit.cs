using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Radionice;

public class RadionicaFavorit
{
    [Key]
    public int Id { get; set; }
    public string KorisnikId { get; set; } = String.Empty;
    [ForeignKey(nameof(KorisnikId))] 
    public virtual Korisnik Korisnik { get; set; } = null!;
    public int RadionicaId { get; set; }
    [ForeignKey(nameof(RadionicaId))]
    public virtual Radionica Radionica { get; set; } = null!;
}
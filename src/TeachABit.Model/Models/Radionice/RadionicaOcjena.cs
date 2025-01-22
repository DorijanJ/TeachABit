using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Radionice;

[Table("RadionicaOcjena")]
public class RadionicaOcjena
{
    [Key]
    public int Id { get; set; }
    
    public int RadionicaId { get; set; }
    [ForeignKey(nameof(RadionicaId))]
    public virtual Radionica Radionica { get; set; } = null!;
    
    public string KorisnikId { get; set; } = string.Empty;
    [ForeignKey(nameof(KorisnikId))]
    public virtual Korisnik Korisnik { get; set; } = null!;
    
    [Range(0, 5, ErrorMessage = "Ocjena mora biti između 0 i 5.")]
    public double Ocjena { get; set; }
}
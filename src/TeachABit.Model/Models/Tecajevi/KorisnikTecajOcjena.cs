using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Tecajevi;

public class KorisnikTecajOcjena
{
    [Key]
    public int Id { get; set; }

    public double? ocjena { get; set; } = null;
    public string KorisnikId { get; set; } = string.Empty;
    [ForeignKey(nameof(KorisnikId))]
    public virtual Korisnik Korisnik { get; set; } = null!;
    
    public int TecajId { get; set; }
    [ForeignKey(nameof(TecajId))]
    public virtual Tecaj Tecaj { get; set; } = null!;
}
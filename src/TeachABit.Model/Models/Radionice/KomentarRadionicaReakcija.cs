using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Radionice;

[Table("KomentarRadionicaReakcija")]
public class KomentarRadionicaReakcija
{
    [Key]
    public int Id { get; set; }
    public string KorisnikId { get; set; } = string.Empty;
    [ForeignKey(nameof(KorisnikId))]
    public virtual Korisnik Korisnik { get; set; } = null!;
    public int KomentarId { get; set; }
    [ForeignKey(nameof(KomentarId))]
    public virtual RadionicaKomentar Komentar { get; set; } = null!;
    public bool Liked { get; set; }
}
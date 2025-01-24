using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Korisnici;

[Table("PrijavaKorisnika")]
public class PrijavaKorisnika
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string PrijavljeniKorisnikId { get; set; } = string.Empty;
    [ForeignKey(nameof(PrijavljeniKorisnikId))]
    public virtual Korisnik PrijavljeniKorisnik { get; set; } = null!;

    [Required]
    public string PrijavioKorisnikId { get; set; } = string.Empty;
    [ForeignKey(nameof(PrijavioKorisnikId))]
    public virtual Korisnik PrijavioKorisnik { get; set; } = null!;

}
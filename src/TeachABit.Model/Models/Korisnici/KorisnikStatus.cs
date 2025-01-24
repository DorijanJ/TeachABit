using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachABit.Model.Models.Korisnici
{
    [Table("KorisnikStatus")]
    public class KorisnikStatus
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;

        public virtual ICollection<Korisnik> Korisnici { get; set; } = [];
    }
}

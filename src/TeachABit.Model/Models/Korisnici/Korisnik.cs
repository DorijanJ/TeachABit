using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Objave;

namespace TeachABit.Model.Models.Korisnici
{
    [Table("AspNetUsers")]
    public class Korisnik : IdentityUser
    {
        public virtual List<Objava> Objave { get; set; } = [];
        public string SlikaUrl { get; set; } = string.Empty;
    }
}

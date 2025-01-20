using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachABit.Model.Models.Korisnici
{
    public class VerifikacijaStatus
    {
        [Key]
        public int Id {  get; set; }
        public required string Naziv {  get; set; }
    }
}

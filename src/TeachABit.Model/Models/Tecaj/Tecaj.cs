using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachABit.Model.Models.Tecaj
{
    [Table("Tecaj")]
    public class Tecaj
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
    }
}
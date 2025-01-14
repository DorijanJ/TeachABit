using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachABit.Model.Models.Tecajevi
{
    [Table("Lekcija")]
    public class Lekcija
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public string Sadrzaj { get; set; } = string.Empty;

        public required int TecajId { get; set; }
        [ForeignKey(nameof(TecajId))]
        public required virtual Tecaj Tecaj { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
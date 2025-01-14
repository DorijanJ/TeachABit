using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Objave;

namespace TeachABit.Model.Models.Tecajevi
{
    [Table("Lekcija")]
    public class Lekcija
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public string Sadrzaj { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public required int TecajId { get; set; }
        [ForeignKey(nameof(TecajId))]
        public required virtual Tecaj Tecaj { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; } = null;
    }
}
using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Objave
{
    public class UpdateObjavaDto
    {
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "Naslov je previše dugačak.")]
        public string Naziv { get; set; } = string.Empty;
        [StringLength(10000, ErrorMessage = "Sadržaj je previše dugačak.")]
        public string Sadrzaj { get; set; } = string.Empty;
    }
}

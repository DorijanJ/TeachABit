using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Objave
{
    public class UpdateKomentarDto
    {
        public int Id { get; set; }
        [StringLength(1000, ErrorMessage = "Sadržaj je previše dugačak.")]
        public string Sadrzaj { get; set; } = string.Empty;
    }
}

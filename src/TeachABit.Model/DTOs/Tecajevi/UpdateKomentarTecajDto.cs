using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Tecajevi
{

    public class UpdateKomentarTecajDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Sadržaj ne smije biti prazan.")]
        [StringLength(1000, ErrorMessage = "Sadržaj je previše dugačak.")]
        [MinLength(1, ErrorMessage = "Sadržaj ne smije biti prazan.")]
        public string Sadrzaj { get; set; } = string.Empty;
    }
}
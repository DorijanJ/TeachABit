using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Tecajevi
{

    public class UpdateKomentarTecajDto
    {
        public int Id { get; set; }
        [StringLength(1000, ErrorMessage = "Sadržaj je previše dugačak.")]
        public string Sadrzaj { get; set; } = string.Empty;
    }
}
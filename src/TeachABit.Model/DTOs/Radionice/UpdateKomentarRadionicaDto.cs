using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Radionice;

public class UpdateKomentarRadionicaDto
{
    public int Id { get; set; }
    [StringLength(1000, ErrorMessage = "Sadržaj je previše dugačak.")]
    public string Sadrzaj { get; set; } = string.Empty;
}
using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Tecajevi;

public class LekcijaDto
{
    public int Id { get; set; }
    [StringLength(100, ErrorMessage = "Naslov je previše dugačak.")]
    public string Naziv { get; set; } = string.Empty;
    [StringLength(30000, ErrorMessage = "Sadržaj je previše dugačak.")]
    public string Sadrzaj { get; set; } = string.Empty;

    public DateTime CreatedDateTime { get; set; }
    public int? TecajId { get; set; }
}
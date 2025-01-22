using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Tecajevi;

public class UpdateLekcijaDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Naziv ne smije biti prazan.")]
    [StringLength(500, ErrorMessage = "Naziv je previše dugačak.")]
    [MinLength(1, ErrorMessage = "Naziv ne smije biti prazan.")]
    public string Naziv { get; set; } = string.Empty;

    [Required(ErrorMessage = "Sadržaj ne smije biti prazan.")]
    [StringLength(20000, ErrorMessage = "Sadržaj je previše dugačak.")]
    [MinLength(1, ErrorMessage = "Sadržaj ne smije biti prazan.")]
    public string Sadrzaj { get; set; } = string.Empty;

}
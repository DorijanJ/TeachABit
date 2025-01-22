using System.ComponentModel.DataAnnotations;
using TeachABit.Model.ValidationAttributes;

namespace TeachABit.Model.DTOs.Tecajevi;

public class CreateOrUpdateTecajDto
{
    public int? Id { get; set; } = null;

    [Required(ErrorMessage = "Naziv ne smije biti prazan.")]
    [StringLength(500, ErrorMessage = "Naziv je previše dugačak.")]
    [MinLength(1, ErrorMessage = "Naziv ne smije biti prazan.")]
    public string Naziv { get; set; } = string.Empty;

    [CijenaValidation]
    public decimal? Cijena { get; set; } = null;

    [Required(ErrorMessage = "Opis ne smije biti prazan.")]
    [StringLength(10000, ErrorMessage = "Opis je previše dugačak.")]
    [MinLength(1, ErrorMessage = "Opis ne smije biti prazan.")]
    public string Opis { get; set; } = string.Empty;

    public string? VlasnikId { get; set; } = string.Empty;
    [ImageFile(maxFileSize: 5 * 1024 * 1024)]
    public string? NaslovnaSlikaBase64 { get; set; } = null;
}
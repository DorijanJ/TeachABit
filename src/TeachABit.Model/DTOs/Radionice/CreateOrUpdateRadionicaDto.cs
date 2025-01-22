using System.ComponentModel.DataAnnotations;
using TeachABit.Model.ValidationAttributes;

namespace TeachABit.Model.DTOs.Radionice;

public class CreateOrUpdateRadionicaDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Naziv ne smije biti prazan.")]
    [StringLength(500, ErrorMessage = "Naziv je previše dugačak.")]
    [MinLength(1, ErrorMessage = "Naziv ne smije biti prazan.")]
    public string Naziv { get; set; } = string.Empty;

    [Required(ErrorMessage = "Opis ne smije biti prazan.")]
    [StringLength(10000, ErrorMessage = "Opis je previše dugačak.")]
    [MinLength(1, ErrorMessage = "Opis ne smije biti prazan.")]
    public string Opis { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cijena je obavezna.")]
    [CijenaValidation]
    public decimal Cijena { get; set; } = 0;

    public string? VlasnikId { get; set; } = string.Empty;

    [Required]
    [BuduciDatumValidation]
    public DateTime VrijemeRadionice { get; set; }

    public int? MaksimalniKapacitet { get; set; } = null;

    [ImageFile(maxFileSize: 5 * 1024 * 1024)]
    public string? NaslovnaSlikaBase64 { get; set; } = null;
}
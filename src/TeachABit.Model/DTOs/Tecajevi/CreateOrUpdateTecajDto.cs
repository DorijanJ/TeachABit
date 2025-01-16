using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TeachABit.Model.ValidationAttributes;

namespace TeachABit.Model.DTOs.Tecajevi;

public class CreateOrUpdateTecajDto
{
    public int Id { get; set; }
    [StringLength(100, ErrorMessage = "Naslov je previše dugačak.")]
    public string Naziv { get; set; } = string.Empty;
    [StringLength(30000, ErrorMessage = "Sadržaj je previše dugačak.")]
    public decimal? Cijena { get; set; } = null;
    public string Opis { get; set; } = string.Empty;
    public string? VlasnikId { get; set; } = string.Empty;
    [ImageFile(maxFileSize: 5 * 1024 * 1024)]
    public IFormFile? NaslovnaSlika { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Radionice;

public class RadionicaDto
{
    public int Id { get; set; }
    [Required]
    public string Naziv {get; set;} = String.Empty;
    [Required]
    public string Opis { get; set; } = string.Empty;
    public bool? Favorit { get; set; } = false;
    [Required] 
    public int? Cijena { get; set; } = 0;
    public string VlasnikId { get; set; } = string.Empty;
    public string? VlasnikUsername { get; set; }
}
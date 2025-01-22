using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TeachABit.Model.DTOs.Radionice;

public class RadionicaDto
{
    public int Id { get; set; }
    [Required]
    public string Naziv { get; set; } = string.Empty;
    public string Opis { get; set; } = string.Empty;
    public bool? Favorit { get; set; } = false;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? Cijena { get; set; } = null;
    public DateTime VrijemeRadionice { get; set; }
    public int MaksimalniKapacitet { get; set; }
    public string VlasnikId { get; set; } = string.Empty;
    public string? VlasnikUsername { get; set; }
    public string? VlasnikProfilnaSlikaVersion { get; set; }
    public string? NaslovnaSlikaVersion { get; set; } = null;
}
namespace TeachABit.Model.DTOs.Radionice;

public class RadionicaDto
{
    public int Id { get; set; }
    public string Naziv { get; set; } = String.Empty;
    public string VlasnikId { get; set; } = string.Empty;
    public string? VlasnikUsername { get; set; }
    public string? VlasnikProfilnaSlikaVersion { get; set; }
}
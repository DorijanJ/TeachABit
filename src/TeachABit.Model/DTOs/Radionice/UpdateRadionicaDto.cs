namespace TeachABit.Model.DTOs.Radionice;

public class UpdateRadionicaDto
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public string Opis { get; set; } = string.Empty;
    public int Cijena { get; set; } = 0;
    public DateTime VrijemeRadionice { get; set; }
    public int MaksimalniKapacitet { get; set; }
}
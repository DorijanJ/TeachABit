using System.ComponentModel.DataAnnotations;


namespace TeachABit.Model.DTOs.Radionice;

public class UpdateRadionicaDto
{
    public int Id {get; set;}
    [Required]
    public string Naziv {get; set;} = String.Empty;
    [Required]
    public string Opis { get; set; } = string.Empty;
    [Required] 
    public int Cijena { get; set; } = 0;
}
using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.Models.Radionice;

public class Radionica
{
    [Key]
    public int Id {get; set;}
    public string Naziv {get; set;}
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachABit.Model.Models.Radionice;

[Table("Radionica")]
public class Radionica
{
    [Key]
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
}
using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Objave
{
    public class ObjavaDto
    {
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "Naslov je previše dugačak.")]
        public string Naziv { get; set; } = string.Empty;
        [StringLength(10000, ErrorMessage = "Sadržaj je previše dugačak.")]
        public string Sadrzaj { get; set; } = string.Empty;
        public string VlasnikId { get; set; } = string.Empty;
        public string? VlasnikUsername { get; set; }
        public string? VlasnikProfilnaSlikaVersion { get; set; }
    }
}

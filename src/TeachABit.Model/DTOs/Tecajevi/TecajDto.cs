using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TeachABit.Model.DTOs.Tecajevi
{
    public class TecajDto
    {
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "Naslov je previše dugačak.")]
        public string Naziv { get; set; } = string.Empty;
        public bool? Favorit { get; set; } = false;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Cijena { get; set; } = null;
        public bool IsPublished { get; set; } = false;
        public string VlasnikId { get; set; } = string.Empty;
        public string? VlasnikUsername { get; set; }
        public string? VlasnikProfilnaSlikaVersion { get; set; }
        public bool? Kupljen { get; set; } = false;
        public string Opis { get; set; } = string.Empty;
        public double Ocjena { get; set; } = 0;
        public int? OcjenaTrenutna { get; set; } = 0;
        public string? NaslovnaSlikaVersion { get; set; } = null;
        public List<LekcijaDto>? Lekcije { get; set; } = [];
    }
}
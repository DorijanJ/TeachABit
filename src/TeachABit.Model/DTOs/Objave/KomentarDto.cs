using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TeachABit.Model.DTOs.Objave
{
    public class KomentarDto
    {
        public int Id { get; set; }
        [StringLength(1000, ErrorMessage = "Sadržaj je previše dugačak.")]
        public string Sadrzaj { get; set; } = string.Empty;
        public string VlasnikId { get; set; } = string.Empty;
        public string VlasnikUsername { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; }
        public string? VlasnikProfilnaSlikaVersion { get; set; }
        public int ObjavaId { get; set; }
        public int? NadKomentarId { get; set; } = null;
        public List<KomentarDto> PodKomentarList { get; set; } = [];
        public int LikeCount { get; set; } = 0;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Liked { get; set; } = null;
    }
}

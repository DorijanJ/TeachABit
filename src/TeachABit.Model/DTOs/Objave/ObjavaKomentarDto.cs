using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TeachABit.Model.DTOs.Objave
{
    public class ObjavaKomentarDto
    {
        public int Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? OznacenTocan { get; set; } = null;

        [Required(ErrorMessage = "Sadržaj ne smije biti prazan.")]
        [StringLength(1000, ErrorMessage = "Sadržaj je previše dugačak.")]
        [MinLength(1, ErrorMessage = "Sadržaj ne smije biti prazan.")]
        public string Sadrzaj { get; set; } = string.Empty;

        public string VlasnikId { get; set; } = string.Empty;
        public string VlasnikUsername { get; set; } = string.Empty;
        public DateTime? CreatedDateTime { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? LastUpdatedDateTime { get; set; } = null;

        public bool IsDeleted { get; set; }
        public string? VlasnikProfilnaSlikaVersion { get; set; }
        public int ObjavaId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? NadKomentarId { get; set; } = null;

        public List<ObjavaKomentarDto> PodKomentarList { get; set; } = [];
        public int LikeCount { get; set; } = 0;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Liked { get; set; } = null;
    }
}

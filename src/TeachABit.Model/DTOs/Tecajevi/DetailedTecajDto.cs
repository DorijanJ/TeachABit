using System.Text.Json.Serialization;

namespace TeachABit.Model.DTOs.Tecajevi;

public class DetailedTecajDto
{ 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<LekcijaDto>? Lekcije { get; set; } = null;
}
using System.Text.Json.Serialization;

namespace TeachABit.Model.DTOs.Objave
{
    public class DetailedObjavaDto : ObjavaDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<KomentarDto>? Komentari { get; set; } = null;
    }
}

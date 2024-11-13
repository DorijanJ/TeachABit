namespace TeachABit.Model.DTOs.Objave
{
    public class KomentarDto
    {
        public int Id { get; set; }
        public string Sadrzaj { get; set; } = string.Empty;
        public required string VlasnikId { get; set; }
        public required string VlasnikUsername { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}

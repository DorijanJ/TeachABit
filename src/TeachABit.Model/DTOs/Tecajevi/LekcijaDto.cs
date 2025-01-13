namespace TeachABit.Model.DTOs.Tecajevi;

public class LekcijaDto
{
    public class LecijaDto
    {
        public int Id { get; set; }
        public string Sadrzaj { get; set; } = string.Empty;
        public required string VlasnikId { get; set; }
        public required string VlasnikUsername { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
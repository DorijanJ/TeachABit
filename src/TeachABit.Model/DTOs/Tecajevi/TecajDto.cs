namespace TeachABit.Model.DTOs.Tecajevi
{
    public class TecajDto
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public required string VlasnikId { get; set; }
        public required string VlasnikUsername { get; set; }
        public List<LekcijaDto> Lekcije { get; set; } = [];
    }
}
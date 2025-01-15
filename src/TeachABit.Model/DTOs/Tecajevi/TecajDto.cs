namespace TeachABit.Model.DTOs.Tecajevi
{
    public class TecajDto
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public bool? Favorit { get; set; } = false;
        public int? Cijena { get; set; } = 0;
        public bool isPublished{ get; set; }=false;
        public string VlasnikId { get; set; } = string.Empty;
        public string? VlasnikUsername { get; set; }
        public List<LekcijaDto>? Lekcije { get; set; } = [];
    }
}
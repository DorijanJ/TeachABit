namespace TeachABit.Model.DTOs.Objave
{
    public class ObjavaDto
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public string Sadrzaj { get; set; } = string.Empty;
        public string VlasnikId { get; set; } = string.Empty;
        public string? VlasnikUsername { get; set; }
    }
}

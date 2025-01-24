using TeachABit.Model.DTOs.Uloge;

namespace TeachABit.Model.DTOs.Korisnici
{
    public class KorisnikDto
    {
        public string Username { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string ProfilnaSlikaVersion { get; set; } = string.Empty;
        public int? VerifikacijaStatusId { get; set; } = null;
        public string? VerifikacijaStatusNaziv { get; set; } = null;
        public List<UlogaDto> Roles { get; set; } = [];
        public string? KorisnikStatus { get; set; } = string.Empty;
        public bool Verificiran { get; set; } = false;
        public int? KorisnikStatusId { get; set; } = null;
        public int BrojPrijava { get; set; } = 0;
    }
}

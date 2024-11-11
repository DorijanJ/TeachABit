using Microsoft.AspNetCore.Http;

namespace TeachABit.Model.DTOs.Korisnici
{
    public class UpdateKorisnikDto
    {
        public string Username { get; set; } = string.Empty;
        public IFormFile? ProfilnaSlika { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Korisnici
{
    public class UpdateKorisnikDto
    {
        [Required(ErrorMessage = "Username ne smije biti prazan.")]
        [StringLength(50, ErrorMessage = "Username predugačak.")]
        [MinLength(1, ErrorMessage = "Username ne smije biti prazan.")]
        public string Username { get; set; } = string.Empty;

        [ImageFile(maxFileSize: 5 * 1024 * 1024)]
        public string? ProfilnaSlikaBase64 { get; set; }
    }
}

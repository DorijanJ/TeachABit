using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Korisnici
{
    public class UpdateKorisnikDto
    {
        [StringLength(50, ErrorMessage = "Email/Korisnično ime predugačko.")]
        [MinLength(1, ErrorMessage = "Email/Username ne smije biti prazan.")]
        [Required(ErrorMessage = "Email/Username ne smije biti prazan.")]
        public string Username { get; set; } = string.Empty;

        [ImageFile(maxFileSize: 5 * 1024 * 1024)]
        public string? ProfilnaSlikaBase64 { get; set; }
    }
}

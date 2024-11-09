using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Authentication
{
    public class LoginAttemptDto
    {
        [Required(ErrorMessage = "Email/Username ne smije biti prazan.")]
        public string Credentials { get; set; } = string.Empty;
        [Required(ErrorMessage = "Lozinka ne smije biti prazna.")]
        public string Password { get; set; } = string.Empty;
    }
}

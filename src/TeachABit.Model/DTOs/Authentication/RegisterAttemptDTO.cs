using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Authentication
{
    public class RegisterAttemptDto
    {
        [Required(ErrorMessage = "Email ne smije biti prazan.")]
        [EmailAddress(ErrorMessage = "Neispravan Email.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username ne smije biti prazan.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lozinka ne smije biti prazna.")]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,16}$", ErrorMessage = "Lozinka nije dovoljno kompleksna.")]
        public string Password { get; set; } = string.Empty;
    }
}

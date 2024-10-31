using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Authentication
{
    public class LoginAttemptDto
    {
        [Required(ErrorMessage = "Credentials can't be empty")]
        public string Credentials { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password can't be empty")]
        public string Password { get; set; } = string.Empty;
    }
}

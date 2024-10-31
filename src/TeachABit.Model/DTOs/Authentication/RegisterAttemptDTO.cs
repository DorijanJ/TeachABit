using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Authentication
{
    public class RegisterAttemptDto
    {
        [Required(ErrorMessage = "Email can't be empty.")]
        [EmailAddress(ErrorMessage = "Invalid email.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username can't be empty.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password can't be empty.")]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,16}$", ErrorMessage = "Password doesn't meet requirements.")]
        public string Password { get; set; } = string.Empty;
    }
}

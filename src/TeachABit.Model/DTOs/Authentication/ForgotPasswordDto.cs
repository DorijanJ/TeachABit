using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Authentication
{
    public class ForgotPasswordDto
    {
        [EmailAddress(ErrorMessage = "Neispravan Email.")]
        public string Email { get; set; } = string.Empty;
    }
}

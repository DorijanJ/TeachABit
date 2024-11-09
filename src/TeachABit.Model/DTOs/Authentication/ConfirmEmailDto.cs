using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Authentication
{
    public class ConfirmEmailDto
    {
        [EmailAddress(ErrorMessage = "Neispravan Email.")]
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}

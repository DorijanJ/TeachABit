using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Authentication
{
    public class ConfirmEmailDto
    {
        [EmailAddress(ErrorMessage = "Neispravan Email.")]
        [StringLength(50, ErrorMessage = "Email predugačak.")]
        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
    }
}

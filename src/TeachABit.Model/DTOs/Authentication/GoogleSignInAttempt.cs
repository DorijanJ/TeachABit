using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Authentication
{
    public class GoogleSignInAttempt
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}

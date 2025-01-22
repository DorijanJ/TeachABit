using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Authentication
{
    public class ResetPasswordDto
    {
        [EmailAddress(ErrorMessage = "Neispravan Email.")]
        [StringLength(50, ErrorMessage = "Email predugačak.")]
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lozinka ne smije biti prazna.")]
        [MinLength(1, ErrorMessage = "Lozinka ne smije biti prazna.")]
        public string Password { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ArgumentNullException.ThrowIfNull(validationContext);

            if (Password.Length < 8 || Password.Length > 16)
            {
                yield return new ValidationResult("Lozinka mora biti dugačka između 8 i 16 znakova.", new[] { nameof(Password) });
            }

            if (!Password.Any(char.IsUpper))
            {
                yield return new ValidationResult("Lozinka mora sadržavati barem jedno veliko slovo.", new[] { nameof(Password) });
            }

            if (!Password.Any(char.IsLower))
            {
                yield return new ValidationResult("Lozinka mora sadržavati barem jedno malo slovo.", new[] { nameof(Password) });
            }

            if (!Password.Any(char.IsDigit))
            {
                yield return new ValidationResult("Lozinka mora sadržavati barem jedan broj.", new[] { nameof(Password) });
            }
        }
    }
}

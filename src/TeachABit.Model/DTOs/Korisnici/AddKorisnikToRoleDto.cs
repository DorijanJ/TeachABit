using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.DTOs.Korisnici
{
    public class AddKorisnikToRoleDto
    {
        [Required(ErrorMessage = "Uloga ne smije biti prazna.")]
        [StringLength(100, ErrorMessage = "Uloga predugačka.")]
        [MinLength(1, ErrorMessage = "Uloga ne smije biti prazna.")]
        public string RoleName { get; set; } = string.Empty;
    }
}

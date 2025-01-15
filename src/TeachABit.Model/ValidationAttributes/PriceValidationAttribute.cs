using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.ValidationAttributes
{
    public class PriceValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is decimal price && price > 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Neispravna cijena.");
        }
    }
}

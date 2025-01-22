using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.ValidationAttributes
{
    public class CijenaValidation : ValidationAttribute
    {
        public CijenaValidation() : base("Cijena mora biti između 1 i 2000.") { }

        public override bool IsValid(object? value)
        {
            if (value is decimal decimalValue)
            {
                return decimalValue >= 1 && decimalValue <= 2000;
            }
            return false;
        }
    }
}
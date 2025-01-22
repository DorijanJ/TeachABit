using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.ValidationAttributes
{
    public class BuduciDatumValidation : ValidationAttribute
    {
        public BuduciDatumValidation() : base("Datum mora biti u budućnosti.") { }

        public override bool IsValid(object? value)
        {
            if (value is DateTime dateTimeValue)
            {
                return dateTimeValue > DateTime.Now;
            }
            return false;
        }
    }
}
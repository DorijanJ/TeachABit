using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TeachABit.Model.ValidationAttributes
{
    public class ImageFileAttribute(long maxFileSize) : ValidationAttribute
    {
        private readonly string[] _permittedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".jfif"];
        private readonly string[] _imageMimeTypes = ["image/jpeg", "image/png", "image/gif", "image/jfif"];
        private readonly long _maxFileSize = maxFileSize;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not IFormFile file)
            {
                return ValidationResult.Success;
            }

            if (file.Length > _maxFileSize)
            {
                return new ValidationResult($"Datoteka ne smije biti preko {_maxFileSize / 1024 / 1024} MB.");
            }

            if (!_imageMimeTypes.Contains(file.ContentType.ToLower()))
            {
                return new ValidationResult("Dozvoljeni tipovi datoteke su JPEG, PNG, GIF, JFIF");
            }

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!_permittedExtensions.Contains(extension))
            {
                return new ValidationResult("Tip datoteke nije dozvoljena.");
            }

            return ValidationResult.Success;
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class ImageFileAttribute : ValidationAttribute
{
    private readonly long _maxFileSize;

    public ImageFileAttribute(long maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        var base64Image = value as string;
        if (string.IsNullOrWhiteSpace(base64Image))
            return new ValidationResult("Invalid image data.");

        var base64Regex = new Regex(@"^data:image\/[a-z]+;base64,");
        base64Image = base64Regex.Replace(base64Image, string.Empty);

        try
        {
            var imageBytes = Convert.FromBase64String(base64Image);
            if (imageBytes.Length > _maxFileSize)
                return new ValidationResult($"File size exceeds the limit of {_maxFileSize} bytes.");
        }
        catch
        {
            return new ValidationResult("Invalid base64 image format.");
        }

        return ValidationResult.Success;
    }
}

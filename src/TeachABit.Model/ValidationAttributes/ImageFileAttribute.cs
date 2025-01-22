using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public partial class ImageFileAttribute : ValidationAttribute
{
    private readonly long _maxFileSize;

    public ImageFileAttribute(long maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || (value is string str && string.IsNullOrEmpty(str)))
            return ValidationResult.Success;

        var base64Image = value as string;
        if (string.IsNullOrWhiteSpace(base64Image))
            return new ValidationResult("Slika nije valjana.");

        var base64Regex = MyRegex();
        base64Image = base64Regex.Replace(base64Image, string.Empty);

        try
        {
            var imageBytes = Convert.FromBase64String(base64Image);
            if (imageBytes.Length > _maxFileSize)
                return new ValidationResult($"Slika je veća od {_maxFileSize} MB.");
        }
        catch
        {
            return new ValidationResult("Base64 format slike nije valjan.");
        }

        return ValidationResult.Success;
    }

    [GeneratedRegex(@"^data:image\/[a-z]+;base64,")]
    private static partial Regex MyRegex();
}

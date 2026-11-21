using FluentValidation;

public class UploadProductImageCommandValidator
    : AbstractValidator<UploadProductImageCommand>
{
    private static readonly string[] AllowedExtensions =
    [
        ".jpg",
        ".jpeg",
        ".png",
        ".webp"
    ];

    private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

    public UploadProductImageCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0);

        RuleFor(x => x.FileName)
            .NotEmpty()
            .Must(HaveValidExtension)
            .WithMessage("Invalid image extension.");

        RuleFor(x => x.FileSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(MaxFileSize)
            .WithMessage("Image size must be less than or equal to 5 MB.");

        RuleFor(x => x.FileStream)
            .NotNull();
    }

    private static bool HaveValidExtension(string fileName)
    {
        string extension = Path.GetExtension(fileName);

        return AllowedExtensions.Contains(
            extension,
            StringComparer.OrdinalIgnoreCase);
    }
}
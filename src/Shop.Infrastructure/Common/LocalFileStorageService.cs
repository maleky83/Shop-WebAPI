using Shop.Application.Interfaces;

namespace Shop.Infrastructure.Common;

public class ProductImageStorage : IProductImageStorage
{
    private readonly string _imagesPath;

    public ProductImageStorage()
    {
        _imagesPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "images",
            "products");
    }

    public async Task<string> SaveAsync(
        string fileName,
        CancellationToken cancellationToken)
    {
        Directory.CreateDirectory(_imagesPath);

        var extension = Path.GetExtension(fileName).ToLowerInvariant();

        var allowedExtensions = new[]
        {
            ".jpg", ".jpeg", ".png", ".webp"
        };

        if (!allowedExtensions.Contains(extension))
            throw new InvalidOperationException("The photo format is not allowed.");

        var newFileName = $"{Guid.NewGuid()}{extension}";

        var filePath = Path.Combine(_imagesPath, newFileName);

        await using var fileStream = new FileStream(
            filePath,
            FileMode.CreateNew,
            FileAccess.Write,
            FileShare.None,
            81920,
            useAsync: true);

        return $"/images/products/{newFileName}";
    }
}

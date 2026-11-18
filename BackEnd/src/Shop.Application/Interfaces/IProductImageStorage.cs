namespace Shop.Application.Interfaces;

public interface IProductImageStorage
{
    Task<string> SaveAsync(
        string fileName,
        CancellationToken cancellationToken);
}

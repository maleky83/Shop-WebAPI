namespace Shop.Application.Interfaces.Repositories.Shopping;

public interface ICartRepository
{
    Task DeleteAsync(Cart entity);
    Task<Cart?> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
}

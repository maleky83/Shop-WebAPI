using Shop.Application.Interfaces;

namespace Shop.Infrastructure.Persistence;

public class UnitOfWork(ShopDbContext dbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}

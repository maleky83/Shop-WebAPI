using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Repositories.Shopping;

namespace Shop.Infrastructure.Persistence.Repositories.Shopping;

internal class CartRepository(ShopDbContext dbContext) : ICartRepository
{
    public async Task<Cart?> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        Cart? cart = await dbContext.Carts
         .Include(c => c.CartItems)
         .ThenInclude(c => c.Product)
         .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);

        return cart;
    }

    public async Task DeleteAsync(Cart entity)
    {
        dbContext.Carts.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task CreateAsync(Cart entity)
    {
        dbContext.Carts.Add(entity);
    }
}

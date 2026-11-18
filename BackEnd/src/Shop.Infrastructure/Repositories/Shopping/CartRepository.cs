using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Domain.Entities.Carts;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Repositories.Shopping;

internal class CartRepository(ShopDbContext dbContext) : ICartRepository
{
    public async Task<Cart?> GetByUserIdAsync(string userId)
    {
        Cart? cart = await dbContext.Carts
         .Include(c => c.CartItems)
         .ThenInclude(c => c.Product)
         .FirstOrDefaultAsync(c => c.UserId == userId);

        return cart;
    }

    public async Task DeleteAsync(Cart entity)
    {
        dbContext.Carts.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

}

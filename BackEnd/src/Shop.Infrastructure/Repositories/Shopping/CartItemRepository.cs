using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Domain.Entities.Carts;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Repositories.Shopping;

internal class CartItemRepository(ShopDbContext dbContext) : ICartItemRepository
{
    public async Task<IEnumerable<CartItem>> GetAllByCartIdAsync(int cartId)
    {
        List<CartItem> cartItems = await dbContext
            .CartItems
            .Where(ci => ci.CartId == cartId)
            .ToListAsync();

        return cartItems;
    }

    public async Task<CartItem?> GetByIdAsync(int cartItemId)
    {
        CartItem? cartItem = await dbContext
            .CartItems
            .FirstOrDefaultAsync(ci => ci.Id == cartItemId);

        return cartItem;
    }

    public async Task AddAsync(string userId, CartItem entity)
    {
        Cart? cart = await dbContext.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId
            };

            await dbContext.Carts.AddAsync(cart);
            await dbContext.SaveChangesAsync();
        }

        CartItem? cartItem = cart.CartItems
            .FirstOrDefault(ci => ci.ProductId == entity.ProductId);

        if (cartItem != null)
        {
            cartItem.Quantity += entity.Quantity;
        }

        else
        {
            cart.CartItems.Add(entity);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(CartItem entity)
    {
        dbContext.CartItems.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}

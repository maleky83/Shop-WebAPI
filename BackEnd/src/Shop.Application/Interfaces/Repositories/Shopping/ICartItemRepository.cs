using Shop.Domain.Entities.Carts;

namespace Shop.Application.Interfaces.Repositories.Shopping;

public interface ICartItemRepository
{
    Task AddAsync(string userId, CartItem entity);
    Task DeleteAsync(CartItem entity);
    Task<IEnumerable<CartItem>> GetAllByCartIdAsync(int cartId);
    Task<CartItem?> GetByIdAsync(int cartItemId);
    Task SaveChangesAsync();
}

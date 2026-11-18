using Shop.Domain.Entities.Orders;

namespace Shop.Application.Interfaces.Repositories.Shopping;

public interface IOrderRepository
{
    Task<int> AddAsync(Order entity, CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetAllAsync(string userId);
    Task<Order?> GetByIdAsync(string userId, int orderId);
    Task SaveChangeAsync();
}

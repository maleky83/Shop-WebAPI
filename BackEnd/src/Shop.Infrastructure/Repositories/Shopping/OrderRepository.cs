using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Domain.Entities.Orders;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Repositories.Shopping;

internal class OrderRepository(ShopDbContext dbContext) : IOrderRepository
{
    public async Task<int> CreateAsync(Order entity)
    {
        await dbContext.Orders.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<IEnumerable<Order>> GetAllAsync(string userId)
    {
        List<Order> orders = await dbContext.Orders
            .Include(o => o.OrderDetails)
            .Where(o => o.UserId == userId)
            .ToListAsync();

        return orders;
    }

    public async Task<Order?> GetByIdAsync(string userId, int orderId)
    {
        Order? order = await dbContext.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(o => o.Product)
            .FirstOrDefaultAsync(o => o.UserId == userId && o.Id == orderId);

        return order;
    }

    public async Task SaveChangeAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}

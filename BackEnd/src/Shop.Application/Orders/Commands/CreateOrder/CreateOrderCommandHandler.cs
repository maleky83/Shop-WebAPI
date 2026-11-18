using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Domain.Entities.Orders;
using Shop.Domain.Enums;
using Shop.Domain.Exceptions;

namespace Shop.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    ILogger<CreateOrderCommandHandler> logger,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork,
    ICartRepository cartRepository
    ) : IRequestHandler<CreateOrderCommand, int>
{
    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating order for user {UserId}", request.UserId);

        Cart? cart = await cartRepository.GetByUserIdAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException(nameof(Cart), "there isn't cart for order");

        var order = new Order
        {
            UserId = request.UserId,
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Pending,
        };

        foreach (CartItem cartItem in cart.CartItems)
        {
            order.AddItem(
                cartItem.Product,
                cartItem.Quantity);
        }

        await orderRepository.AddAsync(
            order,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(
            cancellationToken);

        return order.Id;
    }
}

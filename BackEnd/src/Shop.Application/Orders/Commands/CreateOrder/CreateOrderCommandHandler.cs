using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Domain.Entities.Carts;
using Shop.Domain.Entities.Orders;
using Shop.Domain.Enums;
using Shop.Domain.Exceptions;

namespace Shop.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    ILogger<CreateOrderCommandHandler> logger,
    IOrderRepository orderRepository,
    ICartRepository cartRepository
    ) : IRequestHandler<CreateOrderCommand, int>
{
    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating order for user {UserId}", request.UserId);

        Cart? cart = await cartRepository.GetByUserIdAsync(request.UserId)
            ?? throw new NotFoundException(nameof(Cart), "there isn't cart for order");

        var order = new Order
        {
            UserId = request.UserId,
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Pending,
        };

        foreach (CartItem cartItem in cart.CartItems)
        {
            var orderDetail = new OrderDetail
            {
                Price = cartItem.Product.Price,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                TotalPrice = cartItem.Quantity * cartItem.Product.Price,
            };

            order.OrderDetails.Add(orderDetail);
        }

        order.TotalPrice = order.OrderDetails.Sum(od => od.TotalPrice);

        return await orderRepository.CreateAsync(order);
    }
}

using MediatR;

namespace Shop.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand(string userId) : IRequest<int>
{
    public string UserId { get; } = userId;
}

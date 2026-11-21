using MediatR;
using Shop.Application.Orders.Dtos;

namespace Shop.Application.Orders.Queries.GetOrderbyId;

public class GetOrderbyIdQuery(string userId, int orderId) : IRequest<OrderDto>
{
    public string UserId { get; } = userId;
    public int OrderId { get; } = orderId;
}

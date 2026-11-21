using MediatR;
using Shop.Application.Orders.Dtos;

namespace Shop.Application.Orders.Queries.GetAllOrders;

public class GetAllOrdersQuery(string userId) : IRequest<IEnumerable<OrderDto>>
{
    public string UserId { get; } = userId;
}

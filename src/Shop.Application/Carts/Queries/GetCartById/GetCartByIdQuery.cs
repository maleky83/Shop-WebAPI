using MediatR;
using Shop.Application.Carts.Dtos;

namespace Shop.Application.Carts.Queries.GetCartById;

public class GetCartByIdQuery(string userId) : IRequest<CartDto>
{
    public string UserId { get; } = userId;
}

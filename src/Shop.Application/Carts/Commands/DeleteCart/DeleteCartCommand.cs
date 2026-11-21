using MediatR;

namespace Shop.Application.Carts.Commands.DeleteCart;

public class DeleteCartCommand(string userId) : IRequest
{
    public string UserId { get; } = userId;
}

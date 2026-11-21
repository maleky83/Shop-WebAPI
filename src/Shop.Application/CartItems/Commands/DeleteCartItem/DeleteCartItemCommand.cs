using MediatR;

namespace Shop.Application.CartItems.Commands.DeleteCartItem;

public class DeleteCartItemCommand(string userId, int cartItemId) : IRequest
{
    public string UserId { get; set; } = userId;
    public int CartItemId { get; set; } = cartItemId;
}

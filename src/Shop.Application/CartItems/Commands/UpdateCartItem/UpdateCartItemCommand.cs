using MediatR;

namespace Shop.Application.CartItems.Commands.UpdateCartItem;

public class UpdateCartItemCommand : IRequest
{
    public string UserId { get; set; } = string.Empty;
    public int CartItemId { get; set; }
    public int Quantity { get; set; }
}

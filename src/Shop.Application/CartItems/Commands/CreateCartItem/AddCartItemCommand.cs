using MediatR;

namespace Shop.Application.CartItems.Commands.CreateCartItem;

public class AddCartItemCommand : IRequest
{
    public string UserId { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

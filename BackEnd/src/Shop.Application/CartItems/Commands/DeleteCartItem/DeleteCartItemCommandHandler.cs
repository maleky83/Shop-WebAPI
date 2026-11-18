using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Domain.Entities.Carts;
using Shop.Domain.Exceptions;

namespace Shop.Application.CartItems.Commands.DeleteCartItem;

public class DeleteCartItemCommandHandler(
    ILogger<DeleteCartItemCommandHandler> logger,
    ICartItemRepository cartItemRepository
    ) : IRequestHandler<DeleteCartItemCommand>
{
    public async Task Handle(
        DeleteCartItemCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting cartItem {CartItemId}", request.CartItemId);

        CartItem? cartItem = await cartItemRepository.GetByIdAsync(request.CartItemId)
            ?? throw new NotFoundException(nameof(CartItem), request.CartItemId.ToString());

        await cartItemRepository.DeleteAsync(cartItem);
    }
}

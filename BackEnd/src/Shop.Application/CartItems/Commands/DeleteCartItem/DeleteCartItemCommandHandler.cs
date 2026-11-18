using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Domain.Exceptions;

namespace Shop.Application.CartItems.Commands.DeleteCartItem;

public class DeleteCartItemCommandHandler(
    ILogger<DeleteCartItemCommandHandler> logger,
    ICartRepository cartRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteCartItemCommand>
{
    public async Task Handle(
        DeleteCartItemCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting cartItem {CartItemId}", request.CartItemId);

        Cart? cart =
            await cartRepository.GetByUserIdAsync(
                request.UserId,
                cancellationToken)
        ?? throw new NotFoundException(
            nameof(Cart),
            request.UserId);

        cart.RemoveItem(request.CartItemId);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

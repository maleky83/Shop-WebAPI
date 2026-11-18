using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Domain.Entities.Carts;
using Shop.Domain.Exceptions;

namespace Shop.Application.CartItems.Commands.UpdateCartItem;

public class UpdateCartItemCommandHandler(
    ILogger<UpdateCartItemCommandHandler> logger,
    ICartItemRepository cartItemRepository,
    IValidator<UpdateCartItemCommand> validator
    ) : IRequestHandler<UpdateCartItemCommand>
{
    public async Task Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating cartItem {CartItemId}", request.CartItemId);

        await validator.ValidateAndThrowAsync(request, cancellationToken);

        CartItem? cartItem = await cartItemRepository
            .GetByIdAsync(request.CartItemId)
            ?? throw new NotFoundException(nameof(CartItem), request.CartItemId.ToString());

        cartItem.Quantity = request.Quantity;

        await cartItemRepository.SaveChangesAsync();
    }
}

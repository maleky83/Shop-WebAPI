using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Domain.Exceptions;

namespace Shop.Application.CartItems.Commands.UpdateCartItem;

public class UpdateCartItemCommandHandler(
    ILogger<UpdateCartItemCommandHandler> logger,
    ICartRepository cartRepository,
    IValidator<UpdateCartItemCommand> validator,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateCartItemCommand>
{
    public async Task Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating cartItem {CartItemId}", request.CartItemId);

        await validator.ValidateAndThrowAsync(request, cancellationToken);

        Cart cart = await cartRepository.GetByUserIdAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException(nameof(Cart), request.UserId);

        cart.UpdateItem(request.CartItemId, request.Quantity);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

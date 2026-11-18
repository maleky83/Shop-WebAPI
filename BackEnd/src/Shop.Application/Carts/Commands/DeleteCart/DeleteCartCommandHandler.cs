using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Domain.Entities.Carts;
using Shop.Domain.Exceptions;

namespace Shop.Application.Carts.Commands.DeleteCart;

public class DeleteCartCommandHandler(
    ILogger<DeleteCartCommandHandler> logger,
    ICartRepository cartRepository
    ) : IRequestHandler<DeleteCartCommand>
{
    public async Task Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting cart user {UserId}", request.UserId);

        Cart? cart = await cartRepository.GetByUserIdAsync(request.UserId)
            ?? throw new NotFoundException(nameof(Cart), "there isn't cart for delete");

        await cartRepository.DeleteAsync(cart);
    }
}

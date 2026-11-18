using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces;
using Shop.Application.Interfaces.Repositories;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Domain.Entities.Catalog;
using Shop.Domain.Exceptions;

namespace Shop.Application.CartItems.Commands.CreateCartItem;

public class CreateCartItemCommandHandler(
    ILogger<CreateCartItemCommandHandler> logger,
    IValidator<CreateCartItemCommand> validator,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    ICartRepository cartRepository
    ) : IRequestHandler<CreateCartItemCommand>
{
    public async Task Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating cartItem by user {UserId}", request.UserId);

        await validator.ValidateAndThrowAsync(request, cancellationToken);

        Product product = await productRepository.GetByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(Product), request.ProductId.ToString());

        Cart cart = await cartRepository.GetByUserIdAsync(
            request.UserId,
            cancellationToken)
            ?? new Cart
            {
                UserId = request.UserId
            };

        cart.AddItem(product, request.Quantity);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Carts.Dtos;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Domain.Exceptions;

namespace Shop.Application.Carts.Queries.GetCartById;

public class GetCartByIdQueryHandler(
    ILogger<GetCartByIdQueryHandler> logger,
    ICartRepository cartRepository,
    IMapper mapper
    ) : IRequestHandler<GetCartByIdQuery, CartDto>
{
    public async Task<CartDto> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting cart for user {UserId}", request.UserId);

        Cart cart = await cartRepository.GetByUserIdAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException(nameof(Cart), "please add product first");

        return mapper.Map<CartDto>(cart);
    }
}

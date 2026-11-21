using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces.Repositories;
using Shop.Application.Products.Dtos;
using Shop.Domain.Entities.Catalog;
using Shop.Domain.Exceptions;

namespace Shop.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler(
    ILogger<GetProductByIdQueryHandler> logger,
    IProductRepository productRepository,
    IMapper mapper
    ) :
    IRequestHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting product {ProductId}", request.Id);

        Product? product = await productRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Product), request.Id.ToString());

        ProductDto productDto = mapper.Map<ProductDto>(product);
        return productDto;
    }
}

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Common;
using Shop.Application.Interfaces.Repositories;
using Shop.Application.Products.Dtos;
using Shop.Domain.Entities.Catalog;

namespace Shop.Application.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(
    ILogger<GetAllProductsQueryHandler> logger,
    IProductRepository productRepository,
    IMapper mapper,
    IValidator<GetAllProductsQuery> validator
    ) :
    IRequestHandler<GetAllProductsQuery, PagedResult<ProductDto>>
{
    public async Task<PagedResult<ProductDto>> Handle(
        GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all products {@Products}", request);

        await validator.ValidateAndThrowAsync(request, cancellationToken);

        (IEnumerable<Product>? products, var itemsCount) = await productRepository
            .GetAllMatchingAsync(
                request.SearchPhrase,
                request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection
                );

        IEnumerable<ProductDto> productDtos = mapper.Map<IEnumerable<ProductDto>>(products);

        var result = new PagedResult<ProductDto>(productDtos, itemsCount, request.PageSize, request.PageNumber);

        return result;
    }
}

using MediatR;
using Shop.Application.Common;
using Shop.Application.Constants;
using Shop.Application.Products.Dtos;

namespace Shop.Application.Products.Queries.GetAllProducts;

public class GetAllProductsQuery : IRequest<PagedResult<ProductDto>>
{
    public string? SearchPhrase { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}

using MediatR;
using Shop.Application.Products.Dtos;

namespace Shop.Application.Products.Queries.GetProductById;

public class GetProductByIdQuery(int id) : IRequest<ProductDto>
{
    public int Id { get; } = id;
}

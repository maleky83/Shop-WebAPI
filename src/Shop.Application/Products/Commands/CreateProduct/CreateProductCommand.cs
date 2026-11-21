using MediatR;
using Shop.Application.Products.Dtos;

namespace Shop.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<ProductDto>
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public bool IsActive { get; set; } = true;

    public int CategoryId { get; set; }
}

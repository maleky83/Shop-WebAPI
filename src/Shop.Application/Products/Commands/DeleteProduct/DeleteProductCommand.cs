using MediatR;

namespace Shop.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommand(int id) : IRequest
{
    public int Id { get; } = id;
}

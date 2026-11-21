using MediatR;
using Shop.Application.Categories.Dtos;

namespace Shop.Application.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQuery(int id) : IRequest<CategoryDto>
{
    public int Id { get; } = id;
}

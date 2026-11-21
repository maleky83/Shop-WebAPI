using MediatR;
using Shop.Application.Categories.Dtos;

namespace Shop.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
{
}

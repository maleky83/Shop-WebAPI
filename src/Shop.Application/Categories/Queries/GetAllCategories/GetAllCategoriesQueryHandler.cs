using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Categories.Dtos;
using Shop.Application.Interfaces.Repositories;
using Shop.Domain.Entities.Catalog;

namespace Shop.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler(
    ILogger<GetAllCategoriesQueryHandler> logger,
    ICategoryRepository categoryRepository,
    IMapper mapper
    ) :
    IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    public async Task<IEnumerable<CategoryDto>> Handle(
        GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all categories");

        IEnumerable<Category> categories = await categoryRepository.GetAllAsync();

        IEnumerable<CategoryDto> categoriesDto =
            mapper.Map<IEnumerable<CategoryDto>>(categories);

        return categoriesDto;
    }
}

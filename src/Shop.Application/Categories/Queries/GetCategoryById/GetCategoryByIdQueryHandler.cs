using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Categories.Dtos;
using Shop.Application.Interfaces.Repositories;
using Shop.Domain.Entities.Catalog;
using Shop.Domain.Exceptions;

namespace Shop.Application.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler(
    ILogger<GetCategoryByIdQueryHandler> logger,
    IMapper mapper,
    ICategoryRepository categoryRepository
    ) : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Get category by id");

        Category category = await categoryRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Category), request.Id.ToString());

        CategoryDto categoryDto = mapper.Map<CategoryDto>(category);

        return categoryDto;
    }
}

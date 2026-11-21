using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Categories.Dtos;
using Shop.Application.Categories.Queries.GetAllCategories;
using Shop.Application.Categories.Queries.GetCategoryById;

namespace Shop.Api.Controllers;

[ApiController]
[Route("Categories")]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        IEnumerable<CategoryDto> categories = await mediator.Send(new GetAllCategoriesQuery());

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory([FromRoute] int id)
    {
        CategoryDto category = await mediator.Send(new GetCategoryByIdQuery(id));

        return Ok(category);
    }
}

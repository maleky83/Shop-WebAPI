using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Common;
using Shop.Application.Constants;
using Shop.Application.Products.Commands.CreateProduct;
using Shop.Application.Products.Commands.DeleteProduct;
using Shop.Application.Products.Commands.UpdateProduct;
using Shop.Application.Products.Dtos;
using Shop.Application.Products.Queries.GetAllProducts;
using Shop.Application.Products.Queries.GetProductById;

namespace Shop.Api.Controllers;

[ApiController]
[Route("products")]
[Authorize]
public class ProductsController(
    IMediator mediator
    ) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(
        [FromQuery] GetAllProductsQuery query)
    {
        PagedResult<ProductDto> products = await mediator.Send(query);

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct([FromRoute] int id)
    {
        ProductDto product = await mediator.Send(new GetProductByIdQuery(id));

        return Ok(product);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> CreateProduct(
       CreateProductCommand command)
    {
        ProductDto product = await mediator.Send(command);

        return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] int id,
        [FromBody] UpdateProductCommand command)
    {
        command.Id = id;

        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<ActionResult> DeleteProduct([FromRoute] int id)
    {
        await mediator.Send(new DeleteProductCommand(id));

        return NoContent();
    }

    [HttpPost("{id:int}/image")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> UploadProductImage(
    [FromRoute] int id,
    IFormFile file,
    CancellationToken cancellationToken)
    {
        if (file is null)
            return BadRequest("Image file is required.");

        await using Stream stream = file.OpenReadStream();

        string imageUrl = await mediator.Send(
            new UploadProductImageCommand(
                id,
                stream,
                file.FileName,
                file.Length),
            cancellationToken);

        return Ok(new
        {
            imageUrl
        });
    }

}

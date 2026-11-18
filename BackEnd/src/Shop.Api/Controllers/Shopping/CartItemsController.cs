using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.CartItems.Commands.CreateCartItem;
using Shop.Application.CartItems.Commands.DeleteCartItem;
using Shop.Application.CartItems.Commands.UpdateCartItem;
using Shop.Application.Users;

namespace Shop.Api.Controllers.Shopping;

[ApiController]
[Route("cartItems")]
[Authorize]
public class CartItemsController(
    IMediator mediator, IUserContext userContext
    ) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateCartItem(
       [FromBody] CreateCartItemCommand command)
    {
        CurrentUser? user = userContext.GetCurrentUser();

        command.UserId = user!.Id;

        await mediator.Send(command);

        return Created();
    }

    [HttpPatch("{cartItemId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UpdateCartItem(
       [FromRoute] int cartItemId,
       [FromBody] UpdateCartItemCommand command)
    {
        CurrentUser? user = userContext.GetCurrentUser();

        command.UserId = user!.Id;
        command.CartItemId = cartItemId;

        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{cartItemId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteCartItem([FromRoute] int cartItemId)
    {
        CurrentUser? user = userContext.GetCurrentUser();

        await mediator.Send(new DeleteCartItemCommand(user!.Id, cartItemId));

        return NoContent();
    }
}

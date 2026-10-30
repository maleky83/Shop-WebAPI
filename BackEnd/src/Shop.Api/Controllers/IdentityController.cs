using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Constants;
using Shop.Application.Users.Commands.AssignUserRole;
using Shop.Application.Users.Commands.UnAssignUserRole;
using Shop.Application.Users.Commands.UpdateUserDetails;

namespace Shop.Api.Controllers;

[Route("identity")]
[ApiController]
[Authorize(Roles = UserRoles.Admin)]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("userRole")]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("userRole")]
    public async Task<IActionResult> UnAssignUserRole(UnassignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}

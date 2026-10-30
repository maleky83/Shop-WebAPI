using MediatR;

namespace Shop.Application.Users.Commands.UnAssignUserRole;

public class UnassignUserRoleCommand : IRequest
{
    public string UserEmail { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}

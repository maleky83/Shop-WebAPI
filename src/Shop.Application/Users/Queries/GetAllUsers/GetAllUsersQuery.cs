using MediatR;
using Shop.Application.Users.Dtos;

namespace Shop.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
{
}

using MediatR;
using Shop.Application.Users.Dtos;

namespace Shop.Application.Users.Queries.GetUserById;

public class GetUserByIdQuery(string id) : IRequest<UserDto>
{
    public string Id { get; } = id;
}

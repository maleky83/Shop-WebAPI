using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Shop.Application.Users.Dtos;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;

namespace Shop.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(
    ILogger<GetUserByIdQueryHandler> logger,
    UserManager<User> userManager,
    IMapper mapper
    ) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting user by id {Id}", request.Id);

        var user = await userManager.FindByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(User), request.Id);

        var userDto = mapper.Map<UserDto>(user);

        var roles = await userManager.GetRolesAsync(user);
        userDto.Roles = roles;

        return userDto;
    }
}

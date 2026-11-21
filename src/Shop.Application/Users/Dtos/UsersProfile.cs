using AutoMapper;
using Shop.Application.Users.Commands.CreateUser;
using Shop.Application.Users.Commands.UpdateUser;
using Shop.Domain.Entities;

namespace Shop.Application.Users.Dtos;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<CreateUserCommand, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForAllMembers(options =>
            {
                options.Condition((src, dest, srcMember) =>
                    srcMember != null);
            });

        CreateMap<UpdateUserCommand, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForAllMembers(options =>
            {
                options.Condition((src, dest, srcMember) =>
                    srcMember != null);
            });
    }
}

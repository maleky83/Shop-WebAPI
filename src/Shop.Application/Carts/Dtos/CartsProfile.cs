using AutoMapper;

namespace Shop.Application.Carts.Dtos;

public class CartsProfile : Profile
{
    public CartsProfile()
    {
        CreateMap<Cart, CartDto>()
            .ForMember(d => d.CartId, opt =>
            {
                opt.MapFrom(src => src.Id);
            }).ForMember(d => d.TotalPrice, opt =>
            {
                opt.MapFrom(src => src.CartItems.Sum(ci => ci.Price * ci.Quantity));
            })
            .ForMember(dest => dest.UserId, opt =>
            {
                opt.MapFrom(src => src.UserId);
            });
    }
}

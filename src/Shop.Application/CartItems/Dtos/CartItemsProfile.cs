using AutoMapper;
using Shop.Application.CartItems.Commands.CreateCartItem;

namespace Shop.Application.CartItems.Dtos;

public class CartItemsProfile : Profile
{
    public CartItemsProfile()
    {
        CreateMap<AddCartItemCommand, CartItem>();

        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.CartItemId, opt =>
            {
                opt.MapFrom(src => src.Id);
            })
            .ForMember(dest => dest.TotalPrice, opt =>
            {
                opt.MapFrom(src => src.Quantity * src.Price);
            })
            .ForMember(dest => dest.ProductName, opt =>
            {
                opt.MapFrom(src => src.Product.Name);
            });
    }
}

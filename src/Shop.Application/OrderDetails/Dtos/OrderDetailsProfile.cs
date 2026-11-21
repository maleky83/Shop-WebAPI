using AutoMapper;
using Shop.Domain.Entities.Orders;

namespace Shop.Application.OrderDetails.Dtos;

public class OrderDetailsProfile : Profile
{
    public OrderDetailsProfile()
    {
        CreateMap<OrderDetail, OrderDetailDto>()
            .ForMember(dest => dest.OrderDetailId,
                opt => opt.MapFrom(src => src.Id));
    }
}

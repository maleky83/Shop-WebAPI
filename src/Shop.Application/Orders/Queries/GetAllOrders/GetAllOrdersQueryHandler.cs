using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Application.Orders.Dtos;
using Shop.Domain.Entities.Orders;

namespace Shop.Application.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler(
    IOrderRepository orderRepository,
    IMapper mapper,
    ILogger<GetAllOrdersQueryHandler> logger
    ) :
    IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(
        GetAllOrdersQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting order user {UserId}", request.UserId);

        IEnumerable<Order> orders = await orderRepository.GetAllAsync(request.UserId);

        IEnumerable<OrderDto> orderDtos = orders
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => mapper.Map<OrderDto>(o)).ToList();

        return orderDtos;
    }
}

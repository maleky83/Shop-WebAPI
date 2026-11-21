using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Application.Orders.Dtos;
using Shop.Domain.Entities.Orders;
using Shop.Domain.Exceptions;

namespace Shop.Application.Orders.Queries.GetOrderbyId;

public class GetOrderbyIdQueryHandler(
        ILogger<GetOrderbyIdQueryHandler> logger,
        IOrderRepository orderRepository,
        IMapper mapper
    ) : IRequestHandler<GetOrderbyIdQuery, OrderDto>
{
    public async Task<OrderDto> Handle(
        GetOrderbyIdQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Getting order {OrderId} for user {UserId}",
            request.OrderId, request.UserId);

        Order order = await orderRepository.GetByIdAsync(request.UserId, request.OrderId)
            ?? throw new NotFoundException(nameof(Order), request.OrderId.ToString());

        OrderDto orderDto = mapper.Map<OrderDto>(order);

        return orderDto;
    }
}

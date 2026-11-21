using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Orders.Commands.CreateOrder;
using Shop.Application.Orders.Dtos;
using Shop.Application.Orders.Queries.GetAllOrders;
using Shop.Application.Orders.Queries.GetOrderbyId;
using Shop.Application.Users;

namespace Shop.Api.Controllers.Shopping;

[ApiController]
[Route("orders")]
[Authorize]
public class OrdersController(IMediator mediator, IUserContext userContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    {
        CurrentUser? user = userContext.GetCurrentUser();

        IEnumerable<OrderDto> orderDtos = await mediator.Send(new GetAllOrdersQuery(user!.Id));

        return Ok(orderDtos);
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<OrderDto>> GetOrder(int orderId)
    {
        CurrentUser? user = userContext.GetCurrentUser();

        OrderDto order = await mediator.Send(new GetOrderbyIdQuery(user!.Id, orderId));

        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder()
    {
        CurrentUser? user = userContext.GetCurrentUser();

        var id = await mediator.Send(new CreateOrderCommand(user!.Id));

        return NoContent();
    }
}

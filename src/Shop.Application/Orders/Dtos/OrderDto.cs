using Shop.Domain.Enums;

namespace Shop.Application.Orders.Dtos;

public class OrderDto
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public decimal TotalPrice { get; set; }

    public int ItemCount { get; set; }
}

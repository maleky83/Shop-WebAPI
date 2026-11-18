using Shop.Domain.Entities.Catalog;
using Shop.Domain.Enums;

namespace Shop.Domain.Entities.Orders;

public class Order
{
    private readonly List<OrderDetail> _orderDetails = [];

    public IReadOnlyCollection<OrderDetail> OrderDetails =>
        _orderDetails;

    public decimal TotalPrice =>
        _orderDetails.Sum(x => x.TotalPrice);

    public void AddItem(
        Product product,
        int quantity)
    {
        var detail = new OrderDetail
        {
            ProductId = product.Id,
            ProductName = product.Name,
            Price = product.Price,
            Quantity = quantity,
            TotalPrice = product.Price * quantity
        };

        _orderDetails.Add(detail);
    }
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string UserId { get; set; } = string.Empty;

    public User User { get; set; } = default!;

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

}

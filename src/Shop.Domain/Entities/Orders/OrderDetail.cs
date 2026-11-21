using Shop.Domain.Entities.Catalog;

namespace Shop.Domain.Entities.Orders;

public class OrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public Order Order { get; set; } = default!;

    public int ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public string ProductName { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal TotalPrice { get; set; }
}

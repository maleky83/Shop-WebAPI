namespace Shop.Application.CartItems.Dtos;

public record CartItemDto
{
    public required string CartItemId { get; set; }
    public decimal TotalPrice { get; set; }
    public string ProductId { get; set; } = default!;
    public string? ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

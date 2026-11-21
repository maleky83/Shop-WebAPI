using Shop.Application.CartItems.Dtos;

namespace Shop.Application.Carts.Dtos;

public class CartDto
{
    public int CartId { get; set; }
    public string UserId { get; set; } = default!;
    public ICollection<CartItemDto> CartItems { get; set; } = [];
    public decimal TotalPrice { get; set; }
}

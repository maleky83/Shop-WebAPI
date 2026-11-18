using Shop.Domain.Entities.Catalog;
using System.ComponentModel.DataAnnotations;

public class CartItem
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CartId { get; set; }

    public Cart Cart { get; set; } = default!;

    public int ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public void IncreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ValidationException(
                "Quantity must be greater than zero.");

        Quantity += quantity;
    }

    public void ChangeQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ValidationException(
                "Quantity must be greater than zero.");

        Quantity = quantity;
    }
}
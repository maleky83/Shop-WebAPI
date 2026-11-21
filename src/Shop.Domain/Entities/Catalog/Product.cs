using FluentValidation;
using Shop.Domain.Entities.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities.Catalog;

public class Product
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public bool IsActive { get; set; } = true;

    public int CategoryId { get; set; }
    public string? ImageUrl { get; set; }

    #region Stock

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ValidationException("Quantity must be greater than zero.");

        Quantity += quantity;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ValidationException("Quantity must be greater than zero.");

        if (Quantity < quantity)
            throw new ValidationException("Insufficient stock.");

        Quantity -= quantity;
    }

    public void UpdateStock(int quantity)
    {
        if (quantity < 0)
            throw new ValidationException("Quantity cannot be negative.");

        Quantity = quantity;
    }

    #endregion

    #region Relations

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = default!;

    public ICollection<OrderDetail> OrderDetails { get; set; } = [];

    public ICollection<CartItem> CartItems { get; set; } = [];

    #endregion
}
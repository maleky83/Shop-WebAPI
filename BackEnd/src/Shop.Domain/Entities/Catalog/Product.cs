using System.ComponentModel.DataAnnotations.Schema;
using Shop.Domain.Entities.Orders;

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

    #region Relations

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = default!;

    public ICollection<OrderDetail> OrderDetails { get; set; } = [];

    public ICollection<CartItem> CartItems { get; set; } = [];

    #endregion
}

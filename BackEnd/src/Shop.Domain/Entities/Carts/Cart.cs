using Shop.Domain.Entities;
using Shop.Domain.Entities.Catalog;
using Shop.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

public class Cart
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string UserId { get; set; } = default!;

    public User User { get; set; } = null!;

    public ICollection<CartItem> CartItems { get; set; } = [];

    public void AddItem(Product product, int quantity)
    {
        if (quantity <= 0)
            throw new ValidationException(
                "Quantity must be greater than zero.");

        CartItem? existingItem =
            CartItems.FirstOrDefault(
                x => x.ProductId == product.Id);

        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(quantity);
            existingItem.Price = product.Price;
            return;
        }

        CartItems.Add(new CartItem
        {
            ProductId = product.Id,
            Product = product,
            Quantity = quantity,
            Price = product.Price
        });
    }

    public void UpdateItem(int cartItemId, int quantity)
    {
        if (quantity <= 0)
            throw new ValidationException(
                "Quantity must be greater than zero.");

        CartItem? item = CartItems.FirstOrDefault(
            x => x.Id == cartItemId);

        if (item is null)
            throw new NotFoundException(
                nameof(CartItem),
                cartItemId.ToString());

        item.ChangeQuantity(quantity);
    }

    public void RemoveItem(int cartItemId)
    {
        CartItem? item =
            CartItems.FirstOrDefault(
                x => x.Id == cartItemId);

        if (item is null)
            throw new ValidationException(
                "Cart item was not found.");

        CartItems.Remove(item);
    }
}
using FluentValidation;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Catalog;
using Shop.Domain.Exceptions;

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
            CartItems.FirstOrDefault(x => x.ProductId == product.Id);

        if (existingItem != null)
        {
            product.DecreaseStock(quantity);

            existingItem.IncreaseQuantity(quantity);
            existingItem.Price = product.Price;

            return;
        }

        product.DecreaseStock(quantity);

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

        int oldQuantity = item.Quantity;

        if (quantity > oldQuantity)
        {
            item.Product.DecreaseStock(quantity - oldQuantity);
        }
        else if (quantity < oldQuantity)
        {
            item.Product.IncreaseStock(oldQuantity - quantity);
        }

        item.ChangeQuantity(quantity);
    }

    public void RemoveItem(int cartItemId)
    {
        CartItem? item =
            CartItems.FirstOrDefault(
                x => x.Id == cartItemId);

        if (item is null)
            throw new NotFoundException(
                nameof(Cart), cartItemId.ToString());

        CartItems.Remove(item);

        item.Product.IncreaseStock(item.Quantity);
    }
}
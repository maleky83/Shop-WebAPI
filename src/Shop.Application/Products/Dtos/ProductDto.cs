namespace Shop.Application.Products.Dtos;

public class ProductDto
{
    public required int ProductId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public required int CategoryId { get; set; }
    public string? ImageUrl { get; set; }
}

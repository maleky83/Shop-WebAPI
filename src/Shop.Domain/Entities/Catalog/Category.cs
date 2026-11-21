namespace Shop.Domain.Entities.Catalog;

public class Category
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    #region Relations

    public ICollection<Product> Products { get; set; } = [];

    #endregion
}

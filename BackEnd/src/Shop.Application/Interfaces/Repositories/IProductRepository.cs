using Shop.Application.Constants;
using Shop.Domain.Entities.Catalog;

namespace Shop.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product> CreateAsync(Product entity);
    Task DeleteAsync(Product entity);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<(IEnumerable<Product>, int)> GetAllMatchingAsync(
        string? searchPhrase,
        int? pageSize,
        int? pageNumber,
        string? sortBy,
        SortDirection sortDirection
        );
    Task<Product?> GetByIdAsync(int id);
}

using Shop.Application.Constants;
using Shop.Domain.Entities.Catalog;

namespace Shop.Application.Repositories;

public interface IProductRepository
{
    Task<Product> AddAsync(Product entity);
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
    Task SaveChangesAsync();
}

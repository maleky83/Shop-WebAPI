using Microsoft.EntityFrameworkCore;
using Shop.Application.Constants;
using Shop.Application.Repositories;
using Shop.Domain.Entities.Catalog;
using Shop.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories;

internal class ProductRepository(ShopDbContext dbContext) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        IEnumerable<Product> products = await dbContext.Products.ToListAsync();

        return products;
    }

    public async Task<(IEnumerable<Product>, int)> GetAllMatchingAsync(
        string? searchPhrase,
        int? pageSize,
        int? pageNumber,
        string? sortBy,
        SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Products
            .Where(
                r => searchPhraseLower == null
                || r.Name.ToLower().Contains(searchPhraseLower)
                || r.Description.ToLower().Contains(searchPhraseLower));

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Product, object>>>
            {
                {nameof(Product.Name).ToLower() ,p=>p.Name },
                {nameof(Product.Description).ToLower(),p=> p.Description },
                {nameof(Product.Price).ToLower(),p=>p.Price },
                {nameof(Product.Quantity).ToLower(),p=>p.Quantity }
            };

            Expression<Func<Product, object>> selectedColumn = columnsSelector[sortBy.ToLower()];

            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var totalCount = await baseQuery.CountAsync();

        if (pageSize.HasValue && pageNumber.HasValue)
        {
            baseQuery = baseQuery
                .Skip(pageSize.Value * (pageNumber.Value - 1))
                .Take(pageSize.GetValueOrDefault());
        }

        var products = await baseQuery
            .ToListAsync();

        return (products, totalCount);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        Product? product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        return product;
    }

    public async Task DeleteAsync(Product entity)
    {
        dbContext.Products.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<Product> AddAsync(Product entity)
    {
        await dbContext.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }
}

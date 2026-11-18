using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Repositories;
using Shop.Domain.Entities.Catalog;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Repositories;

internal class CategoryRepository(ShopDbContext dbContext) : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        List<Category> categories = await dbContext.Categories.ToListAsync();

        return categories;
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        Category? category = await dbContext
            .Categories.FirstOrDefaultAsync(c => c.Id == id);

        return category;
    }
}

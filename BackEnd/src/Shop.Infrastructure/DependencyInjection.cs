using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Interfaces;
using Shop.Application.Repositories;
using Shop.Application.Repositories.Shopping;
using Shop.Application.Users;
using Shop.Domain.Entities;
using Shop.Infrastructure.Authorization;
using Shop.Infrastructure.Common;
using Shop.Infrastructure.Persistence;
using Shop.Infrastructure.Repositories;
using Shop.Infrastructure.Repositories.Shopping;
using Shop.Infrastructure.Seeders;
using Shop.Infrastructure.Users;

namespace Shop.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastrucure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ShopDb");
        var connectionStringHost = configuration.GetConnectionString("HostDb");

        services.AddDbContext<ShopDbContext>(options => options
            .UseSqlServer(connectionString)
            .EnableSensitiveDataLogging()
            );

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<ShopUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<ShopDbContext>();

        services.AddScoped<IShopSeeder, ShopSeeder>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductImageStorage, ProductImageStorage>();
        services.AddScoped<IUserContext, UserContext>();

        services.AddHttpContextAccessor();
    }
}

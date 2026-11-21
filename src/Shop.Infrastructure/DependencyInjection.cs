using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Interfaces;
using Shop.Application.Interfaces.Repositories;
using Shop.Application.Interfaces.Repositories.Shopping;
using Shop.Application.Users;
using Shop.Domain.Entities;
using Shop.Infrastructure.Authorization;
using Shop.Infrastructure.Common;
using Shop.Infrastructure.Persistence;
using Shop.Infrastructure.Persistence.Repositories;
using Shop.Infrastructure.Persistence.Repositories.Shopping;
using Shop.Infrastructure.Seeders;
using Shop.Infrastructure.Users;

namespace Shop.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
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

        services.Configure<BearerTokenOptions>(IdentityConstants.BearerScheme, options =>
        {
            options.BearerTokenExpiration = TimeSpan.FromDays(99);
            options.RefreshTokenExpiration = TimeSpan.FromDays(99);
        });

        services.AddScoped<IShopSeeder, ShopSeeder>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductImageStorage, ProductImageStorage>();

        services.AddHttpContextAccessor();
    }
}

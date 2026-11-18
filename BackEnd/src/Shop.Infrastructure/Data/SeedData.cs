using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities.Catalog;

namespace Shop.Infrastructure.Data;

public static class SeedData
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        // Categories
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "men"
            },
            new Category
            {
                Id = 2,
                Name = "women"
            }
        );

        // Products

        modelBuilder.Entity<Product>().HasData(
            // Men's Collection
            new Product
            {
                Id = 1,
                CreatedAt = new DateTime(2026, 1, 5, 10, 30, 0),
                Name = "Men's Classic Black Jacket",
                Description = "A premium black jacket with a modern fit, perfect for everyday wear and cold weather.",
                Price = 89.99m,
                CategoryId = 1,
                ImageUrl = "/images/men/mens_jacket.jpg",
                Quantity = 12,
                IsActive = true
            },
            new Product
            {
                Id = 2,
                CreatedAt = new DateTime(2026, 1, 8, 14, 15, 0),
                Name = "Men's Performance Running Shoes",
                Description = "Lightweight running shoes featuring breathable materials and excellent support for daily workouts.",
                Price = 79.99m,
                CategoryId = 1,
                ImageUrl = "/images/men/mens_shoes.jpg",
                Quantity = 18,
                IsActive = true
            },
            new Product
            {
                Id = 3,
                CreatedAt = new DateTime(2026, 1, 8, 16, 45, 0),
                Name = "Men's Urban Graphic T-Shirt",
                Description = "A comfortable cotton graphic T-shirt designed for casual outfits and everyday activities.",
                Price = 24.99m,
                CategoryId = 1,
                ImageUrl = "/images/men/mens_tee.jpg",
                Quantity = 35,
                IsActive = true
            },
            new Product
            {
                Id = 4,
                CreatedAt = new DateTime(2026, 1, 12, 11, 20, 0),
                Name = "Men's Everyday White Sneakers",
                Description = "Minimalist white sneakers combining a clean design with all-day comfort.",
                Price = 64.99m,
                CategoryId = 1,
                ImageUrl = "/images/men/mens_sneaker.jpg",
                Quantity = 22,
                IsActive = true
            },

            // Women's Collection
            new Product
            {
                Id = 5,
                CreatedAt = new DateTime(2026, 1, 6, 9, 15, 0),
                Name = "Women's Floral Summer Dress",
                Description = "An elegant lightweight floral dress, perfect for summer outings and casual occasions.",
                Price = 69.99m,
                CategoryId = 2,
                ImageUrl = "/images/women/womens_dress.jpg",
                Quantity = 16,
                IsActive = true
            },
            new Product
            {
                Id = 6,
                CreatedAt = new DateTime(2026, 1, 10, 13, 40, 0),
                Name = "Women's Premium Leather Handbag",
                Description = "A stylish leather handbag with a spacious interior and practical compartments for everyday essentials.",
                Price = 109.99m,
                CategoryId = 2,
                ImageUrl = "/images/women/womens_bag.jpg",
                Quantity = 9,
                IsActive = true
            },
            new Product
            {
                Id = 7,
                CreatedAt = new DateTime(2026, 1, 10, 17, 25, 0),
                Name = "Women's Everyday Canvas Tote",
                Description = "A durable and spacious canvas tote bag suitable for shopping, work, and everyday use.",
                Price = 34.99m,
                CategoryId = 2,
                ImageUrl = "/images/women/womens_tote.jpg",
                Quantity = 28,
                IsActive = true
            },
            new Product
            {
                Id = 8,
                CreatedAt = new DateTime(2026, 1, 15, 12, 10, 0),
                Name = "Women's Modern Street Sneakers",
                Description = "Fashionable street-style sneakers designed to provide comfort and versatility throughout the day.",
                Price = 74.99m,
                CategoryId = 2,
                ImageUrl = "/images/women/womens_sneaker.jpg",
                Quantity = 14,
                IsActive = true
            }
        );

    }
}

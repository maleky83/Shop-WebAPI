using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Shop.Application.Products.Dtos;
using Shop.Domain.Entities.Catalog;

namespace Shop.Application.Tests.Products.Dtos;

public class ProductsProfileTests
{
    [Fact]
    public void CreateMap_ForProductToProductDto_MapsCorrectly()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductsProfile>();
        }, NullLoggerFactory.Instance);

        var mapper = configuration.CreateMapper();

        var product = new Product()
        {
            Id = 1,
            Name = "test",
            Description = "test",
            CategoryId = 1,
            Price = 1,
            Quantity = 1,
        };

        // Act
        var productDto = mapper.Map<ProductDto>(product);

        // Assert
        productDto.Should().NotBeNull();
        productDto.ProductId.Should().Be(product.Id);
        productDto.Description.Should().Be(product.Description);
        productDto.Name.Should().Be(product.Name);
        productDto.Price.Should().Be(product.Price);
        productDto.Quantity.Should().Be(product.Quantity);
        productDto.CategoryId.Should().Be(product.CategoryId);
    }
}

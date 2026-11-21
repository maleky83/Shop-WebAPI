using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Shop.Application.Interfaces.Repositories;
using Shop.Application.Products.Commands.CreateProduct;
using Shop.Application.Products.Dtos;
using Shop.Domain.Entities.Catalog;

namespace Shop.Application.Tests.Products.Commands.CreateProduct;

public class CreateProductCommandHandlerTests
{
    [Fact]
    public async Task HandleCreateProduct_ForValidHandle_ReturnProductDto()
    {
        // Arrange
        var mapperMock = new Mock<IMapper>();

        var validatorMock = new Mock<IValidator<CreateProductCommand>>();

        var loggerMock = new Mock<ILogger<CreateProductCommandHandler>>();

        var command = new CreateProductCommand()
        {
            CategoryId = 1,
            Name = "test",
            Description = "test",
            Quantity = 1,
            Price = 2,
        };

        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        categoryRepositoryMock.Setup(c => c.GetByIdAsync(command.CategoryId))
            .ReturnsAsync(new Category());

        var product = new Product();

        var productDto = new ProductDto()
        {
            CategoryId = 1,
            Name = "test",
            Description = "test",
            ProductId = 1,
            Quantity = 1,
            Price = 2,
        };

        mapperMock.Setup(mapper => mapper.Map<Product>(command))
            .Returns(product);

        var productRepositoryMock = new Mock<IProductRepository>();
        productRepositoryMock.Setup(p => p.AddAsync(product))
            .ReturnsAsync(product);

        mapperMock.Setup(mapper => mapper.Map<ProductDto>(product))
            .Returns(productDto);

        var handler = new CreateProductCommandHandler(
            mapperMock.Object,
            loggerMock.Object,
            productRepositoryMock.Object,
            categoryRepositoryMock.Object,
            validatorMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Quantity.Should().Be(productDto.Quantity);
        result.Name.Should().Be(productDto.Name);
        result.Description.Should().Be(productDto.Description);
        result.Price.Should().Be(productDto.Price);
        result.CategoryId.Should().Be(productDto.CategoryId);
        result.ProductId.Should().Be(productDto.ProductId);
    }
}

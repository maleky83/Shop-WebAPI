using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Shop.Application.Interfaces.Repositories;
using Shop.Application.Products.Dtos;
using Shop.Domain.Entities.Catalog;
using System.Net;
using System.Net.Http.Json;

namespace Shop.Api.Tests.Controllers;

public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly int _id;
    private readonly HttpClient _client;
    private readonly Mock<IProductRepository> _productRepository = new();
    public ProductsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(sercices =>
            {
                sercices.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                sercices
                    .Replace(ServiceDescriptor
                        .Scoped(typeof(IProductRepository), _ => _productRepository.Object));
            });
        });

        _id = 99;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetById_ForExistingId_ShouldReturn200Ok()
    {
        // Arrange
        var product = new Product()
        {
            Id = _id,
            Name = "test",
            Description = "test",
        };

        _productRepository.Setup(p => p.GetByIdAsync(_id)).ReturnsAsync(product);

        // Act
        var response = await _client.GetAsync($"/products/{_id}", CancellationToken.None);
        var productDto = await response.Content.ReadFromJsonAsync<ProductDto>(CancellationToken.None);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        productDto.Should().NotBeNull();
        productDto.Name.Should().Be(product.Name);
        productDto.Description.Should().Be(product.Description);
        productDto.ProductId.Should().Be(product.Id);

    }

    [Fact]
    public async Task GetById_ForNonExitstingId_ShouldReturn404NotFound()
    {
        // Arrange
        _productRepository.Setup(p => p.GetByIdAsync(_id))
            .ReturnsAsync((Product?)null);

        // Act
        var response = await _client.GetAsync($"/products/{_id}", CancellationToken.None);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAll_ForValidRequest_ShouldReturn200Ok()
    {
        // Arrange
        var products = new List<Product>();

        _productRepository.Setup(p => p.GetAllAsync())
            .ReturnsAsync(products);

        // Act
        var response = await _client
            .GetAsync("/products?PageSize=5&PageNumber=2", CancellationToken.None);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAll_ForInvalidRequest_Return400BadRequest()
    {
        // Arrange
        _productRepository.Setup(p => p.GetAllAsync())
            .ReturnsAsync((List<Product>)null!);

        // Act
        var response = await _client
            .GetAsync("/products?PageSize=-1&PageNumber=-2&SortBy=test", CancellationToken.None);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}

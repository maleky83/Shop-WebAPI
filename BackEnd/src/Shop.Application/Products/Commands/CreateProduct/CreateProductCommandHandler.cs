using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces;
using Shop.Application.Interfaces.Repositories;
using Shop.Application.Products.Dtos;
using Shop.Domain.Entities.Catalog;
using Shop.Domain.Exceptions;

namespace Shop.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IMapper mapper,
    ILogger<CreateProductCommandHandler> logger,
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateProductCommand> validator
    ) : IRequestHandler<CreateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating product {Product}", request);

        await validator.ValidateAndThrowAsync(request, cancellationToken);

        Category? category = await categoryRepository.GetByIdAsync(request.CategoryId)
            ?? throw new NotFoundException(nameof(Category), request.CategoryId.ToString());

        Product entity = mapper.Map<Product>(request);

        Product product = await productRepository.CreateAsync(entity);

        await unitOfWork.SaveChangesAsync();

        return mapper.Map<ProductDto>(product);
    }
}

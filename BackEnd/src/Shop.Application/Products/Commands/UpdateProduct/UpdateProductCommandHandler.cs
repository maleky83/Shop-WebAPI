using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces;
using Shop.Application.Interfaces.Repositories;
using Shop.Domain.Entities.Catalog;
using Shop.Domain.Exceptions;

namespace Shop.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(
    ILogger<UpdateProductCommandHandler> logger,
    IMapper mapper,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateProductCommand> validator
    ) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product {ProductId}", request.Id);

        await validator.ValidateAndThrowAsync(request, cancellationToken);

        Product? product = await productRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Product), request.Id.ToString());

        mapper.Map(request, product);

        await unitOfWork.SaveChangesAsync();
    }
}

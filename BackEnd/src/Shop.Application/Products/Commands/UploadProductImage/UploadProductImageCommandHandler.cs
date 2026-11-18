using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces;
using Shop.Application.Interfaces.Repositories;
using Shop.Domain.Entities.Catalog;
using Shop.Domain.Exceptions;

namespace Shop.Application.Products.Commands.UploadProductImage;

public sealed class UploadProductImageCommandHandler(
    ILogger<UploadProductImageCommandHandler> logger,
    IProductRepository productRepository,
    IProductImageStorage storage,
    IValidator<UploadProductImageCommand> validator
    )
    : IRequestHandler<UploadProductImageCommand, string>
{
    public async Task<string> Handle(
        UploadProductImageCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Uploading image {@Request}", request);

        await validator.ValidateAndThrowAsync(request, cancellationToken);

        Product product = await productRepository
            .GetByIdAsync(request.ProductId)
            ?? throw new NotFoundException(
                nameof(Product),
                request.ProductId.ToString());

        var imageUrl = await storage.SaveAsync(
            request.Content,
            request.FileName,
            cancellationToken);

        product.ImageUrl = imageUrl;

        await productRepository.SaveChangesAsync();

        return imageUrl;
    }
}

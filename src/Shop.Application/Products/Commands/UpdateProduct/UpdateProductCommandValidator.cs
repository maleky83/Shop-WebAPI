using FluentValidation;

namespace Shop.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 100)
            .When(x => !string.IsNullOrEmpty(x.Name));
    }
}

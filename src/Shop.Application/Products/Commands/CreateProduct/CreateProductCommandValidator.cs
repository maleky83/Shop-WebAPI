using FluentValidation;

namespace Shop.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 100);

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => x.Description is not null);

        RuleFor(x => x.Price)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .NotEmpty();

        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}

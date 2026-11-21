using FluentValidation;

namespace Shop.Application.CartItems.Commands.CreateCartItem;

public class AddCartItemCommandValidator : AbstractValidator<AddCartItemCommand>
{
    public AddCartItemCommandValidator()
    {
        RuleFor(ci => ci.UserId)
            .NotEmpty();

        RuleFor(ci => ci.Quantity)
            .NotEmpty();

        RuleFor(ci => ci.ProductId)
            .NotEmpty();
    }
}

using FluentValidation;
using Shop.Domain.Entities.Catalog;

namespace Shop.Application.Products.Queries.GetAllProducts;

public class GetAllProductsQueryValidator : AbstractValidator<GetAllProductsQuery>
{
    private readonly int[] AllowPageSizes = [5, 10, 15, 30];
    private readonly string[] AllowSortByColumnNames =
        [nameof(Product.Name).ToLower(),
        nameof(Product.Description).ToLower(),
        nameof(Product.Price).ToLower(),
        nameof(Product.Quantity).ToLower()];

    public GetAllProductsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x)
        .Must(x => x.PageSize.HasValue == x.PageNumber.HasValue)
        .WithMessage("PageSize and PageNumber must be provided together.");


        RuleFor(r => r.PageSize)
            .Must(value => AllowPageSizes.Contains(value.GetValueOrDefault()))
            .When(value => value.PageSize.HasValue)
            .WithMessage($"Page size must be in [{string.Join(",", AllowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => AllowSortByColumnNames.Contains(value?.ToLower()))
            .When(r => r.SortBy != null)
            .WithMessage(
                $"Sort by is optional, or must be in [{string.Join(",", AllowSortByColumnNames)}]");
    }
}

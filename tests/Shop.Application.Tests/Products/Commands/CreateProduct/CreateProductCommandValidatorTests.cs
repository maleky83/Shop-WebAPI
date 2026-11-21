using FluentValidation.TestHelper;
using Shop.Application.Products.Commands.CreateProduct;

namespace Shop.Application.Tests.Products.Commands.CreateProduct;

public class CreateProductCommandValidatorTests
{
    [Fact]
    public void Validtor_ForValidCommand_ShouldNotHaveErros()
    {
        // Arrange
        var command = new CreateProductCommand()
        {
            Name = "test",
            Description = "test",
            CategoryId = 1,
            Quantity = 1,
            Price = 1,
        };

        var validator = new CreateProductCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}

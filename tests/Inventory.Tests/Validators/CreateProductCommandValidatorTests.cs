using FluentAssertions;
using FluentValidation.TestHelper;
using Inventory.Application.Products.Commands;
using Inventory.Application.Products.Validators;

namespace Inventory.Tests.Validators;

public class CreateProductCommandValidatorTests
{
    private readonly CreateProductCommandValidator
        _validator;

    public CreateProductCommandValidatorTests()
    {
        _validator =
            new CreateProductCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var command =
            new CreateProductCommand
            {
                Name = string.Empty,
                Price = 100,
                Stock = 10,
                CategoryId = Guid.NewGuid()
            };

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            product => product.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Price_Is_Invalid()
    {
        var command =
            new CreateProductCommand
            {
                Name = "Laptop",
                Price = 0,
                Stock = 10,
                CategoryId = Guid.NewGuid()
            };

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            product => product.Price);
    }
}
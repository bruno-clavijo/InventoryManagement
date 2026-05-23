using FluentValidation;
using Inventory.Application.Products.Commands;

namespace Inventory.Application.Products.Validators;

public class UpdateProductCommandValidator
    : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(product => product.Id)
            .NotEmpty();

        RuleFor(product => product.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(product => product.Price)
            .GreaterThan(0);

        RuleFor(product => product.Stock)
            .GreaterThanOrEqualTo(0);

        RuleFor(product => product.CategoryId)
            .NotEmpty();
    }
}
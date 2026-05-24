using FluentValidation;
using Inventory.Application.Categories.Commands;

namespace Inventory.Application.Categories.Validators;

public class CreateCategoryCommandValidator
    : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(category => category.Name)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(category => category.Description)
        .NotEmpty()
        .MaximumLength(500);
    }
}
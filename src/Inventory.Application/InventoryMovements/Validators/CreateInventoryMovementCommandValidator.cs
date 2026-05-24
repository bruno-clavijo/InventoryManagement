using FluentValidation;
using Inventory.Application.InventoryMovements.Commands;

namespace Inventory.Application.InventoryMovements.Validators;

public class CreateInventoryMovementCommandValidator
    : AbstractValidator<CreateInventoryMovementCommand>
{
    public CreateInventoryMovementCommandValidator()
    {
        RuleFor(movement => movement.ProductId)
            .NotEmpty();

        RuleFor(movement => movement.Quantity)
            .GreaterThan(0);

        RuleFor(movement => movement.Type)
            .Must(type =>
                type == "Entry" ||
                type == "Exit")
            .WithMessage(
                "Movement type must be Entry or Exit");
    }
}
using Inventory.Domain.Common;
using Inventory.Domain.Enums;
using Inventory.Domain.Exceptions;

namespace Inventory.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Guid CategoryId { get; set; }
    public bool IsActive { get; set; } = true;

    public void ApplyInventoryMovement(
    InventoryMovementType movementType,
    int quantity)
    {
        if (quantity <= 0)
        {
            throw new DomainException(
                "Cantidad debe ser mayor a cero.");
        }

        if (movementType == InventoryMovementType.Entry)
        {
            Stock += quantity;
            return;
        }

        if (Stock < quantity)
        {
            throw new DomainException(
                "Stock insuficiente.");
        }

        Stock -= quantity;
    }
}
using Inventory.Domain.Common;
using Inventory.Domain.Enums;

namespace Inventory.Domain.Entities;

public class InventoryMovement : BaseEntity
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public InventoryMovementType Type { get; set; }
}
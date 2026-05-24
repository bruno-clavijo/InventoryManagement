namespace Inventory.Application.InventoryMovements.DTOs;

public class InventoryMovementDto
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public string Type { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }
}
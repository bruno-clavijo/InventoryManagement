using Inventory.Application.InventoryMovements.DTOs;
using MediatR;

namespace Inventory.Application.InventoryMovements.Commands;

public class CreateInventoryMovementCommand
    : IRequest<InventoryMovementDto>
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public string Type { get; set; } = string.Empty;
}
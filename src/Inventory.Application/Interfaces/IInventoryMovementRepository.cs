using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IInventoryMovementRepository
{
    Task<InventoryMovement> CreateAsync(
        InventoryMovement inventoryMovement);

    Task UpdateProductStockAsync(
        Guid productId,
        int newStock);
}
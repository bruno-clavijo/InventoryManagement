using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Inventory.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    DbSet<Category> Categories { get; }

    DbSet<InventoryMovement> InventoryMovements { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken);

    Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken);
}
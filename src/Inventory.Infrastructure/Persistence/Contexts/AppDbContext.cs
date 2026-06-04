using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Inventory.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Inventory.Infrastructure.Persistence.Contexts;

public class AppDbContext
    : DbContext,
      IApplicationDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<InventoryMovement> InventoryMovements => Set<InventoryMovement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public async Task<IDbContextTransaction>
        BeginTransactionAsync(
            CancellationToken cancellationToken)
    {
        return await Database
            .BeginTransactionAsync(
                cancellationToken);
    }
}
using Dapper;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Inventory.Infrastructure.Repositories;

public class InventoryMovementRepository
    : IInventoryMovementRepository
{
    private readonly IConfiguration _configuration;

    public InventoryMovementRepository(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<InventoryMovement> CreateAsync(
        InventoryMovement inventoryMovement)
    {
        const string sql = """
            INSERT INTO InventoryMovements
            (
                Id,
                ProductId,
                Quantity,
                Type,
                CreatedAtUtc
            )
            VALUES
            (
                @Id,
                @ProductId,
                @Quantity,
                @Type,
                @CreatedAtUtc
            )
            """;

        using var connection = new SqlConnection(
            _configuration.GetConnectionString(
                "DefaultConnection"));

        inventoryMovement.Id = Guid.NewGuid();

        inventoryMovement.CreatedAtUtc = DateTime.UtcNow;

        await connection.ExecuteAsync(
            sql,
            inventoryMovement);

        return inventoryMovement;
    }

    public async Task UpdateProductStockAsync(
        Guid productId,
        int newStock)
    {
        const string sql = """
            UPDATE Products
            SET Stock = @NewStock
            WHERE Id = @ProductId
            """;

        using var connection = new SqlConnection(
            _configuration.GetConnectionString(
                "DefaultConnection"));

        await connection.ExecuteAsync(
            sql,
            new
            {
                ProductId = productId,
                NewStock = newStock
            });
    }
}
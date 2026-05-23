using Dapper;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Inventory.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IConfiguration _configuration;

    public ProductRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        const string sql = """
            INSERT INTO Products
            (
                Id,
                Name,
                Description,
                Price,
                Stock,
                CategoryId,
                CreatedAtUtc
            )
            VALUES
            (
                @Id,
                @Name,
                @Description,
                @Price,
                @Stock,
                @CategoryId,
                @CreatedAtUtc
            )
            """;

        using var connection = new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection"));
        product.Id = Guid.NewGuid();
        product.CreatedAtUtc = DateTime.UtcNow;
        await connection.ExecuteAsync(sql, product);

        return product;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        const string sql = """
            UPDATE Products
            SET
                Name = @Name,
                Description = @Description,
                Price = @Price,
                Stock = @Stock,
                CategoryId = @CategoryId
            WHERE Id = @Id
            """;

        using var connection = new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection"));

        var rowsAffected = await connection.ExecuteAsync(
            sql,
            product);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        const string sql = """
            DELETE FROM Products
            WHERE Id = @Id
            """;

        using var connection = new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection"));

        var rowsAffected = await connection.ExecuteAsync(
            sql,
            new { Id = id });

        return rowsAffected > 0;
    }
}
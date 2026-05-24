using Dapper;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Inventory.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IConfiguration _configuration;

    public CategoryRepository(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Category> CreateAsync(
        Category category)
    {
        const string sql = """
            INSERT INTO Categories
            (
                Id,
                Name,
                Description,
                CreatedAtUtc
            )
            VALUES
            (
                @Id,
                @Name,
                @Description,
                @CreatedAtUtc
            )
            """;

        using var connection = new SqlConnection(
            _configuration.GetConnectionString(
                "DefaultConnection"));

        category.Id = Guid.NewGuid();

        category.CreatedAtUtc = DateTime.UtcNow;

        await connection.ExecuteAsync(sql, category);

        return category;
    }
}
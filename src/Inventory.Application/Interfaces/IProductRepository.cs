using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IProductRepository
{
    Task<Product> CreateAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteAsync(Guid id);
}
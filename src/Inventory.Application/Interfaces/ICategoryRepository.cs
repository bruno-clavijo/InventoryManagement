using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);
}
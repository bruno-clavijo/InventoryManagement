using Inventory.Application.Categories.DTOs;
using MediatR;

namespace Inventory.Application.Categories.Commands;

public class CreateCategoryCommand
    : IRequest<CategoryDto>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
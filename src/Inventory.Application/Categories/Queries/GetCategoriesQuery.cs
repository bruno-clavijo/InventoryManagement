using Inventory.Application.Categories.DTOs;
using MediatR;

namespace Inventory.Application.Categories.Queries;

public class GetCategoriesQuery
    : IRequest<IEnumerable<CategoryDto>>
{
}
using Inventory.Application.Products.DTOs;
using MediatR;

namespace Inventory.Application.Products.Queries;

public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
{
}
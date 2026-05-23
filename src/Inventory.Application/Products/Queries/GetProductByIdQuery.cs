using Inventory.Application.Products.DTOs;
using MediatR;

namespace Inventory.Application.Products.Queries;

public class GetProductByIdQuery
    : IRequest<ProductDto?>
{
    public Guid Id { get; set; }
}
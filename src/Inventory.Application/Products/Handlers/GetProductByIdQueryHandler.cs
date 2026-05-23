using Inventory.Application.Interfaces;
using Inventory.Application.Products.DTOs;
using Inventory.Application.Products.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application.Products.Handlers;

public class GetProductByIdQueryHandler
    : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IApplicationDbContext _context;

    public GetProductByIdQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDto?> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(product => product.Id == request.Id)
            .Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
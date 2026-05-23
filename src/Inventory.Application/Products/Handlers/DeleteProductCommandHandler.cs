using Inventory.Application.Interfaces;
using Inventory.Application.Products.Commands;
using MediatR;

namespace Inventory.Application.Products.Handlers;

public class DeleteProductCommandHandler
    : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(
        DeleteProductCommand request,
        CancellationToken cancellationToken)
    {
        return await _productRepository.DeleteAsync(
            request.Id);
    }
}
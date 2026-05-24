using Inventory.Application.Interfaces;
using Inventory.Application.InventoryMovements.Commands;
using Inventory.Application.InventoryMovements.DTOs;
using Inventory.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Enums;
using Inventory.Application.Common.Exceptions;

namespace Inventory.Application.InventoryMovements.Handlers;

public class CreateInventoryMovementCommandHandler
    : IRequestHandler<
        CreateInventoryMovementCommand,
        InventoryMovementDto>
{
    private readonly IApplicationDbContext _context;

    private readonly IInventoryMovementRepository
        _inventoryMovementRepository;

    public CreateInventoryMovementCommandHandler(
        IApplicationDbContext context,
        IInventoryMovementRepository inventoryMovementRepository)
    {
        _context = context;

        _inventoryMovementRepository =
            inventoryMovementRepository;
    }

    public async Task<InventoryMovementDto> Handle(
        CreateInventoryMovementCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(product =>
                product.Id == request.ProductId,
                cancellationToken);

        if (product is null)
        {
            throw new BusinessException("Producto no encontrado");
        }

        var movementType =
            Enum.Parse<InventoryMovementType>(
                request.Type, true);

        var newStock =
            movementType == InventoryMovementType.Entry
            ? product.Stock + request.Quantity
            : product.Stock - request.Quantity;

        if (newStock < 0)
        {
            throw new BusinessException("Stock insuficiente");
        }

        var inventoryMovement =
            new InventoryMovement
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Type = Enum.Parse<InventoryMovementType>(request.Type),
            };

        var createdMovement =
            await _inventoryMovementRepository
                .CreateAsync(inventoryMovement);

        await _inventoryMovementRepository
            .UpdateProductStockAsync(
                product.Id,
                newStock);

        return new InventoryMovementDto
        {
            Id = createdMovement.Id,
            ProductId = createdMovement.ProductId,
            Quantity = createdMovement.Quantity,
            Type = createdMovement.Type.ToString(),
            CreatedAtUtc =
                createdMovement.CreatedAtUtc
        };
    }
}
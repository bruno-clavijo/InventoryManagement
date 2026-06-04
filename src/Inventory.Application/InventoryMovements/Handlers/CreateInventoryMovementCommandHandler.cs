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

    public CreateInventoryMovementCommandHandler(
    IApplicationDbContext context)
    {
        _context = context;
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
                request.Type,
                true);

        var inventoryMovement =
            new InventoryMovement
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Type = movementType,
                CreatedAtUtc = DateTime.UtcNow
            };

        await using var transaction =
            await _context.BeginTransactionAsync(
                cancellationToken);

        try
        {
            product.ApplyInventoryMovement(
                movementType,
                request.Quantity);

            _context.InventoryMovements
                .Add(inventoryMovement);

            await _context.SaveChangesAsync(
                cancellationToken);

            await transaction.CommitAsync(
                cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(
                cancellationToken);

            throw new BusinessException(
                "Stock insuficiente para realizar el movimiento.");
        }

        return new InventoryMovementDto
        {
            Id = inventoryMovement.Id,
            ProductId = inventoryMovement.ProductId,
            Quantity = inventoryMovement.Quantity,
            Type = inventoryMovement.Type.ToString(),
            CreatedAtUtc =
                inventoryMovement.CreatedAtUtc
        };
    }
}
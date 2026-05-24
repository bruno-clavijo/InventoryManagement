using FluentAssertions;
using Inventory.Application.Common.Exceptions;
using Inventory.Application.Interfaces;
using Inventory.Application.InventoryMovements.Commands;
using Inventory.Application.InventoryMovements.Handlers;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Inventory.Tests.InventoryMovements;

public class CreateInventoryMovementCommandHandlerTests
{
    [Fact]
    public async Task Should_Throw_Exception_When_Stock_Is_Insufficient()
    {
        var options =
            new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(
                    databaseName: Guid.NewGuid().ToString())
                .Options;

        await using var context =
            new AppDbContext(options);

        var product =
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Laptop",
                Price = 100,
                Stock = 5,
                CategoryId = Guid.NewGuid()
            };

        context.Products.Add(product);

        await context.SaveChangesAsync();

        var inventoryMovementRepositoryMock =
            new Mock<IInventoryMovementRepository>();

        var handler =
            new CreateInventoryMovementCommandHandler(
                context,
                inventoryMovementRepositoryMock.Object);

        var command =
            new CreateInventoryMovementCommand
            {
                ProductId = product.Id,
                Quantity = 10,
                Type = "Exit"
            };

        Func<Task> action = async () =>
            await handler.Handle(
                command,
                CancellationToken.None);

        await action.Should()
            .ThrowAsync<BusinessException>()
            .WithMessage("Stock insuficiente");
    }
}
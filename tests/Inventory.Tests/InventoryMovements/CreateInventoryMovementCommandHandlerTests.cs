using FluentAssertions;
using Inventory.Application.Common.Exceptions;
using Inventory.Application.Interfaces;
using Inventory.Application.InventoryMovements.Commands;
using Inventory.Application.InventoryMovements.Handlers;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Inventory.Tests.InventoryMovements;

public class CreateInventoryMovementCommandHandlerTests
{
    private static AppDbContext CreateDbContext()
    {
        var options =
            new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString())
                .ConfigureWarnings(w =>
                    w.Ignore(
                        Microsoft.EntityFrameworkCore.Diagnostics
                            .InMemoryEventId
                            .TransactionIgnoredWarning))
                .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Should_Create_Entry_Movement()
    {
        // Arrange

        var context = CreateDbContext();

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Laptop",
            Description = "Test",
            Price = 1000,
            Stock = 10,
            CategoryId = Guid.NewGuid()
        };

        context.Products.Add(product);

        await context.SaveChangesAsync(
            CancellationToken.None);

        var handler =
            new CreateInventoryMovementCommandHandler(
                context);

        var command =
            new CreateInventoryMovementCommand
            {
                ProductId = product.Id,
                Quantity = 5,
                Type = "Entry"
            };

        // Act

        var result =
            await handler.Handle(
                command,
                CancellationToken.None);

        // Assert

        result.Should().NotBeNull();

        result.Quantity.Should().Be(5);

        result.Type.Should().Be("Entry");

        product.Stock.Should().Be(15);

        context.InventoryMovements
            .Count()
            .Should()
            .Be(1);
    }

    [Fact]
    public async Task Should_Throw_When_Product_Not_Found()
    {
        // Arrange

        var context = CreateDbContext();

        var handler =
            new CreateInventoryMovementCommandHandler(
                context);

        var command =
            new CreateInventoryMovementCommand
            {
                ProductId = Guid.NewGuid(),
                Quantity = 5,
                Type = "Entry"
            };

        // Act

        Func<Task> act = async () =>
            await handler.Handle(
                command,
                CancellationToken.None);

        // Assert

        await act.Should()
            .ThrowAsync<BusinessException>()
            .WithMessage(
                "*Producto no encontrado*");
    }

    [Fact]
    public async Task Should_Throw_When_Stock_Is_Insufficient()
    {
        // Arrange

        var context = CreateDbContext();

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Laptop",
            Description = "Test",
            Price = 1000,
            Stock = 5,
            CategoryId = Guid.NewGuid()
        };

        context.Products.Add(product);

        await context.SaveChangesAsync(
            CancellationToken.None);

        var handler =
            new CreateInventoryMovementCommandHandler(
                context);

        var command =
            new CreateInventoryMovementCommand
            {
                ProductId = product.Id,
                Quantity = 10,
                Type = "Exit"
            };

        // Act

        Func<Task> act = async () =>
            await handler.Handle(
                command,
                CancellationToken.None);

        // Assert

        await act.Should()
            .ThrowAsync<BusinessException>()
            .WithMessage(
                "*Stock insuficiente*");
    }
}
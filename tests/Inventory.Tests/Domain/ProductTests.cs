using FluentAssertions;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Domain.Exceptions;

namespace Inventory.Tests.Domain;

public class ProductTests
{
    [Fact]
    public void Should_Increase_Stock_When_Entry()
    {
        var product = new Product
        {
            Stock = 10
        };

        product.ApplyInventoryMovement(
            InventoryMovementType.Entry,
            5);

        product.Stock.Should().Be(15);
    }

    [Fact]
    public void Should_Decrease_Stock_When_Exit()
    {
        var product = new Product
        {
            Stock = 10
        };

        product.ApplyInventoryMovement(
            InventoryMovementType.Exit,
            3);

        product.Stock.Should().Be(7);
    }

    [Fact]
    public void Should_Throw_When_Stock_Is_Insufficient()
    {
        var product = new Product
        {
            Stock = 5
        };

        var action = () =>
            product.ApplyInventoryMovement(
                InventoryMovementType.Exit,
                10);

        action.Should()
            .Throw<DomainException>()
            .WithMessage("*Stock insuficiente*");
    }

    [Fact]
    public void Should_Throw_When_Quantity_Is_Invalid()
    {
        var product = new Product
        {
            Stock = 5
        };

        var action = () =>
            product.ApplyInventoryMovement(
                InventoryMovementType.Entry,
                0);

        action.Should()
            .Throw<DomainException>()
            .WithMessage("*Cantidad debe ser mayor a cero*");
    }

}
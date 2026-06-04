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

    }
}
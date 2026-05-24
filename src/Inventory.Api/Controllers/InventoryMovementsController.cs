using Inventory.Application.InventoryMovements.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Inventory.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InventoryMovementsController
    : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryMovementsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateInventoryMovementCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}
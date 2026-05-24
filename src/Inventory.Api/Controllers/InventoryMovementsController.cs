using Inventory.Application.InventoryMovements.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Inventory.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InventoryMovementsController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryMovementsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new movement.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create(CreateInventoryMovementCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}
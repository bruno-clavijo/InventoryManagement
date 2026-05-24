using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Products.Queries;
using Inventory.Application.Products.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Inventory.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    /// <summary>
    /// Retrieves all products.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetProductsQuery());

        return Ok(result);
    }

    /// <summary>
    /// Retrieves product by Id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(
            new GetProductByIdQuery
            {
                Id = id
            });
        if (result is null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Updates product by Id
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
    {
        if (id != command.Id)
            return BadRequest();
        var result = await _mediator.Send(command);
        if (!result)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Deletes product by Id.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(
            new DeleteProductCommand
            {
                Id = id
            });

        if (!result)
            return NotFound();

        return NoContent();
    }
}
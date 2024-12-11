using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

namespace Tutorial.EntityFrameworkUpdate.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public CategoriesController(ILogger<CategoriesController> logger,
                                IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write)]
    public async Task<ActionResult<ImmutableArray<Category>>> GetAll()
    {
        var request = new GetAllRequest();
        var response = await _mediator.Send(request);

        if (response == null || response.IsEmpty)
            return NoContent();
        else
            return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write)]
    public async Task<ActionResult<Category>> Get([FromRoute] int id)
    {
        if (id <= 0)
            return BadRequest();

        var request = new GetRequest() { Id = id };
        var response = await _mediator.Send(request);

        if (response == null)
            return NotFound();
        else
            return Ok(response);
    }


    [HttpGet]
    [Route("{id:int}/Products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write)]
    public async Task<ActionResult<ImmutableArray<Category>>> GetProducts([FromRoute] int id)
    {
        var request = new GetAllRequest();
        var response = await _mediator.Send(request);

        if (response == null || response.IsEmpty)
            return NoContent();
        else
            return Ok(response);
    }
}

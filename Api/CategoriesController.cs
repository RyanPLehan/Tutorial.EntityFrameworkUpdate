using System.Collections.Immutable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Tutorial.EntityFrameworkUpdate.Api.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

namespace Tutorial.EntityFrameworkUpdate.Api;

[ApiController]
[Route("[controller]")]
//[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public class Item
    {
        public class Add
        {
            public string Name { get; init; } = null;
            public string Description { get; init; } = null;
        }
        public class Update
        {
            public string Description { get; init; } = null;
        }
    }

    public CategoriesController(ILogger<CategoriesController> logger,
                                IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id:int}/Products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write)]
    public async Task<ActionResult<ItemList<Product>>> GetProducts([FromRoute] int id)
    {
        var request = new GetAll();
        var response = await _mediator.Send(request);

        if (response == null || response.IsEmpty)
            return NoContent();
        else
            return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write)]
    public async Task<ActionResult<Category>> Add([FromBody] Item.Add entity)
    {
        if (entity == null)
            return BadRequest();

        var request = new Add()
        {
            Name = entity.Name,
            Description = entity.Description,
        };

        var response = await _mediator.Send(request);

        if (response == null)
            return BadRequest();
        else
            return CreatedAtAction(nameof(GetDetail), new { id = response.Id }, response);
    }


    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write)]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        if (id <= 0)
            return BadRequest();

        var getRequest = new GetById() { Id = id };
        var getResponse = await _mediator.Send(getRequest);

        if (getResponse == null)
            return NotFound();
        else
        {
            var delRequest = new Delete() { Category = getResponse };
            await _mediator.Send(delRequest);
            return Ok();
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write)]
    public async Task<ActionResult<ItemList<Category>>> GetAll()
    {
        var request = new GetAll();
        var response = await _mediator.Send(request);

        if (response == null || response.IsEmpty)
            return NoContent();
        else
        {
            // Bad practice to return just an array of items.  Should include a at least one property
            return Ok(new ItemList<Category>() { Items = response });
        }

    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write)]
    public async Task<ActionResult<Category>> GetDetail([FromRoute] int id)
    {
        if (id <= 0)
            return BadRequest();

        var request = new GetById() { Id = id };
        var response = await _mediator.Send(request);

        if (response == null)
            return NotFound();
        else
            return Ok(response);
    }



}

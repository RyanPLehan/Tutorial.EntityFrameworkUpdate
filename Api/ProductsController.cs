using System.Collections.Immutable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Tutorial.EntityFrameworkUpdate.Api.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;
//using ProductTagReq = Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Requests;

namespace Tutorial.EntityFrameworkUpdate.Api;

[ApiController]
[Route("[controller]")]
//[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public class Item
    {
        public class Add
        {
            public string Name { get; init; }
            public string Description { get; init; }
            public decimal Price { get; init; }
            public int Quantity { get; init; }
            public int CategoryId { get; init; }
        }

        public class Update
        {
            public string Description { get; init; }
            public decimal Price { get; init; }
            public int Quantity { get; init; }
        }

        public class Patch
        {
            public string? Description { get; init; } = null;
            public decimal? Price { get; init; } = null;
            public int? Quantity { get; set; } = null;
        }
    }

    public ProductsController(ILogger<ProductsController> logger,
                              IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize(Roles = AuthorizationRoles.Write + ", " + AuthorizationRoles.Admin)]
    public async Task<ActionResult<Product>> Add([FromBody] Item.Add entity)
    {
        if (entity == null)
            return BadRequest();

        var request = new Add()
        {
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            Quantity = entity.Quantity,
            CategoryId = entity.CategoryId,
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
    //[Authorize(Roles = AuthorizationRoles.Admin)]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        if (id <= 0)
            return BadRequest();

        var request = new DeleteById() { Id = id };
        await _mediator.Send(request);
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write + ", " + AuthorizationRoles.Admin)]
    public async Task<ActionResult<ItemList<Product>>> GetByCategory([FromQuery] int categoryid = 0)
    {
        var request = new FindByCategory() { CategoryId = categoryid };
        var response = await _mediator.Send(request);

        if (response == null || response.IsEmpty)
            return NoContent();
        else
            // Bad practice to return just an array of items.  Should include a at least one property
            return Ok(new ItemList<Product>() { Items = response });

    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write + ", " + AuthorizationRoles.Admin)]
    public async Task<ActionResult<Product>> GetDetail([FromRoute] int id)
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


    [HttpPatch]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(Roles = AuthorizationRoles.Admin)]
    public async Task<ActionResult<Product>> Replace([FromRoute] int id, [FromBody] Item.Patch entity)
    {
        // Demostrate how to use the HTTP Patch method to update any number of fields
        // This example allows the updating of 1 to 3 fields.

        // It should be noted, that I personally do not like using the HTTP Patch method to update specific fields.
        // I believe that it should be used for the purpose of modifying collection items (ie add/delete items to/from a list)

        if (id <= 0)
            return BadRequest();

        var request = new Patch()
        {
            Id = id,
            Description = entity.Description,
            Price = entity.Price,
            Quantity = entity.Quantity,
        };

        var response = await _mediator.Send(request);

        if (response == null)
            return NotFound();
        else
            return Ok(response);
    }


    [HttpPut]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(Roles = AuthorizationRoles.Write + ", " + AuthorizationRoles.Admin)]
    public async Task<ActionResult<Product>> Update([FromRoute] int id, [FromBody] Item.Update entity)
    {
        if (id <= 0)
            return BadRequest();

        var request = new Update()
        {
            Id = id,
            Description = entity.Description,
            Price = entity.Price,
            Quantity = entity.Quantity,
        };

        var response = await _mediator.Send(request);

        if (response == null)
            return NotFound();
        else
            return Ok(response);
    }
}

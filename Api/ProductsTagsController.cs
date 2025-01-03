using System.Collections.Immutable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Tutorial.EntityFrameworkUpdate.Api.Models;
using Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;
using Tutorial.EntityFrameworkUpdate.Domain.Models;

namespace Tutorial.EntityFrameworkUpdate.Api;

[ApiController]
[Route("Products/{productId:int}/tags")]
//[Authorize]
public class ProductTagsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public class Item
    {
        public class Add
        {
            public required string Name { get; init; }
            public string? Value { get; init; }
        }
    }

    public ProductTagsController(ILogger<ProductTagsController> logger,
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
    public async Task<ActionResult<ProductTag>> Add([FromRoute] int productId, [FromBody] Item.Add entity)
    {
        if (entity == null)
            return BadRequest();

        var request = new Add()
        {
            ProductId = productId,
            Name = entity.Name,
            Value = entity.Value,
        };

        var response = await _mediator.Send(request);

        if (response == null)
            return BadRequest();
        else
            return CreatedAtAction(nameof(GetDetail), new { productId, response.Id }, response);
    }


    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize(Roles = AuthorizationRoles.Write + ", " + AuthorizationRoles.Admin)]
    public async Task<ActionResult> Delete([FromRoute] int productId, [FromRoute] int id)
    {
        if (productId <= 0 || id <= 0)
            return BadRequest();

        var request = new DeleteById() { ProductId = productId, Id = id };
        await _mediator.Send(request);
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write + ", " + AuthorizationRoles.Admin)]
    public async Task<ActionResult<TagList>> GetAll([FromRoute] int productId)
    {
        if (productId <= 0)
            return BadRequest();

        var request = new FindByProduct() { ProductId = productId };
        var response = await _mediator.Send(request);

        if (response == null || response.IsEmpty)
            return NoContent();
        else
            return Ok(new TagList() { Tags = response });
    }




    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write + ", " + AuthorizationRoles.Admin)]
    public async Task<ActionResult<ProductTag>> GetDetail([FromRoute] int productId, [FromRoute] int id)
    {
        if (productId <= 0 || id <= 0)
            return BadRequest();

        var request = new GetById { ProductId = productId, Id = id };
        var response = await _mediator.Send(request);

        if (response == null)
            return NotFound();
        else
            return Ok(response);
    }


    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(Roles = AuthorizationRoles.Write + ", " + AuthorizationRoles.Admin)]
    public async Task<ActionResult<TagList>> PatchTags([FromRoute] int productId, [FromBody] TagList tags)
    {
        // Demostrate how to use the HTTP Patch method to modify a collection of items (add/delete to/from a list)

        if (productId <= 0)
            return BadRequest();

        var request = new Patch()
        {
            ProductId = productId,
            Tags = tags.Tags,
        };

        var response = await _mediator.Send(request);

        if (response == null || response.IsEmpty)
            return NotFound();
        else
            return CreatedAtAction(nameof(GetAll), new { productId }, new TagList() { Tags = response });
    }

}

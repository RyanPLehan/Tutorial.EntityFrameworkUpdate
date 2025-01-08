using System.Collections.Immutable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Tutorial.EntityFrameworkUpdate.Api.Models;
using Tutorial.EntityFrameworkUpdate.Application.Categories.Requests;
using ProductReq = Tutorial.EntityFrameworkUpdate.Application.Products.Requests;
using Tutorial.EntityFrameworkUpdate.Application.Models;

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

        public class UpdateFull
        {
            public string Name { get; init; } = null;
            public string Description { get; init; } = null;
        }

        public class UpdatePartial
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize(Roles = AuthorizationRoles.Write + ", " + AuthorizationRoles.Admin)]
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
    //[Authorize(Roles = AuthorizationRoles.Admin)]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        if (id <= 0)
            return BadRequest();

        // Since Categories are cached, we can do a quick lookup to see if it exists
        var getRequest = new GetById() { Id = id };
        var getResponse = await _mediator.Send(getRequest);
        if (getResponse == null)
            return NotFound();

        var delRequest = new DeleteById() { Id = id };
        await _mediator.Send(delRequest);
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write + ", " + AuthorizationRoles.Admin)]
    public async Task<ActionResult<ItemList<Category>>> GetAll()
    {
        var request = new GetAll();
        var response = await _mediator.Send(request);

        if (response == null || response.IsEmpty)
            return NoContent();
        else
            // Bad practice to return just an array of items.  Should include a at least one property
            return Ok(new ItemList<Category>() { Items = response });

    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write + ", " + AuthorizationRoles.Admin)]
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

    [HttpPatch]
    [Route("{oldId:int}/{newId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(Roles = AuthorizationRoles.Admin)]
    public async Task<ActionResult> Replace([FromRoute] int oldId, [FromRoute] int newId)
    {

        // It should be noted, that I personally do not like using the HTTP Patch method to update specific fields.
        // I believe that it should be used for the purpose of modifying collection items (ie add/delete items to/from a list)
        // However, this particular case does update a list of products by replacing one category id for another


        if (oldId <= 0 || newId <= 0)
            return BadRequest();


        ActionResult actionResult;
        try
        {
            var request = new Replace()
            {
                OldCategoryId = oldId,
                NewCategoryId = newId,
            };

            await _mediator.Send(request);
            actionResult = Ok();
        }
        catch
        {
            actionResult = NotFound();
        }

        return actionResult;
    }

    [HttpPut]
    [Route("{id:int}/Full")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(Roles = AuthorizationRoles.Write + ", " + AuthorizationRoles.Admin)]
    public async Task<ActionResult<Category>> Update([FromRoute] int id, [FromBody] Item.UpdateFull entity)
    {
        if (id <= 0)
            return BadRequest();

        var getRequest = new GetById() { Id = id };
        var getResponse = await _mediator.Send(getRequest);

        if (getResponse == null)
            return NotFound();
        else
        {
            var updRequest = new UpdateFullContract() { Id = id, Name = entity.Name, Description = entity.Description };
            var updResponse = await _mediator.Send(updRequest);
            return Ok(updResponse);
        }
    }

    [HttpPut]
    [Route("{id:int}/Partial")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //[Authorize(Roles = AuthorizationRoles.Write + ", " + AuthorizationRoles.Admin)]
    public async Task<ActionResult<Category>> Update([FromRoute] int id, [FromBody] Item.UpdatePartial entity)
    {
        if (id <= 0)
            return BadRequest();

        var getRequest = new GetById() { Id = id };
        var getResponse = await _mediator.Send(getRequest);

        if (getResponse == null)
            return NotFound();
        else
        {
            var updRequest = new UpdatePartialContract() { Id = id, Description = entity.Description };
            var updResponse = await _mediator.Send(updRequest);
            return Ok(updResponse);
        }
    }

    #region Products
    [HttpGet]
    [Route("{id:int}/Products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //[Authorize(Roles = AuthorizationRoles.Read + ", " + AuthorizationRoles.Write + ", " + AuthorizationRoles.Admin)]
    public async Task<ActionResult<ItemList<Product>>> GetProducts([FromRoute] int id)
    {
        if (id <= 0)
            return BadRequest();

        var request = new ProductReq.FindByCategory() { CategoryId = id };
        var response = await _mediator.Send(request);

        if (response == null || response.IsEmpty)
            return NoContent();
        else
            return Ok(response);
    }
    #endregion
}

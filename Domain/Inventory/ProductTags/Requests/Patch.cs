using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Requests;

public class Patch : IRequest<ImmutableArray<ProductTag>>
{
    public required int ProductId { get; init; }
    public IEnumerable<Tag> Tags { get; init; }
}

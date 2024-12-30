using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Requests;

public class FindByTagName : IRequest<ImmutableArray<ProductTag>>
{
    public required string TagName { get; init; }
}

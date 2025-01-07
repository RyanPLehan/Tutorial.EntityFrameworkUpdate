using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;

public class Patch : IRequest<ImmutableArray<ProductTag>>
{
    public required int ProductId { get; init; }
    public IEnumerable<ProductTag> Tags { get; init; }
}

using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;

public class Patch : IRequest<ImmutableArray<ProductTag>>
{
    public required int ProductId { get; init; }
    public IEnumerable<Tag> Tags { get; init; }
}

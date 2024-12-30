using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Requests;

public class FindByProduct : IRequest<ImmutableArray<ProductTag>>
{
    public required int ProductId { get; init; }
}

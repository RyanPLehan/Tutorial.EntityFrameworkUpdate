using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;

public class FindByProduct : IRequest<ImmutableArray<ProductTag>>
{
    public required int ProductId { get; init; }
}

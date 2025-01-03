using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;

public class FindByTagName : IRequest<ImmutableArray<ProductTag>>
{
    public required string TagName { get; init; }
}

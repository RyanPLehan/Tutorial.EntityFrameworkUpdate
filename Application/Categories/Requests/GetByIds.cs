using MediatR;
using System.Collections.Generic;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Categories.Requests;

public class GetByIds : IRequest<ImmutableArray<Category>>
{
    public required IEnumerable<int> Ids { get; init; }
}

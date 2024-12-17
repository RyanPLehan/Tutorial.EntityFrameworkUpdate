using MediatR;
using System.Collections.Generic;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

public class GetByIds : IRequest<ImmutableArray<Category>>
{
    public IEnumerable<int> Ids { get; init; }
}

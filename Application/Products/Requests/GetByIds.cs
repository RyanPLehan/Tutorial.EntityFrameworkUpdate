using MediatR;
using System.Collections.Generic;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Requests;

public class GetByIds : IRequest<ImmutableArray<Product>>
{
    public IEnumerable<int> Ids { get; init; }
}

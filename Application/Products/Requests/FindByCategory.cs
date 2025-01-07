using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Requests;

public class FindByCategory : IRequest<ImmutableArray<Product>>
{
    public required int CategoryId { get; init; }
}

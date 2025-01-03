using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Requests;

public class FindByCategory : IRequest<ImmutableArray<Product>>
{
    public required int CategoryId { get; init; }
}

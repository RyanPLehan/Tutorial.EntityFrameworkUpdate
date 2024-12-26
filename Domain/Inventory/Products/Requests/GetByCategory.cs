using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;

public class GetByCategory : IRequest<ImmutableArray<Product>>
{
    public required int CategoryId { get; init; }
}

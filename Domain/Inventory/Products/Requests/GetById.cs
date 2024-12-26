using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;

public class GetById : IRequest<Product?>
{
    public int Id { get; init; }
}

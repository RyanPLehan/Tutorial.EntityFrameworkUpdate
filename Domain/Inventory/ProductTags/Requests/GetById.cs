using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Requests;

public class GetById : IRequest<ProductTag?>
{
    public required int ProductId { get; init; }
    public required int Id { get; init; }
}

using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Requests;

public class Add : IRequest<ProductTag>
{
    public required int ProductId { get; init; }
    public required string Name { get; init; }
    public string? Value { get; init; } = null;
}

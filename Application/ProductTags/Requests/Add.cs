using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;

public class Add : IRequest<ProductTag>
{
    public required int ProductId { get; init; }
    public required string Name { get; init; }
    public string? Value { get; init; } = null;
}

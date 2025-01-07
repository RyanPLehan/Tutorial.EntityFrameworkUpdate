using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Requests;

public class Add : IRequest<Product>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; } = 0;
    public required int Quantity { get; init; } = 0;
    public required int CategoryId { get; init; }
}

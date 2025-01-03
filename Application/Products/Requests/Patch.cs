using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Requests;

public class Patch : IRequest<Product?>
{
    public required int Id { get; init; }
    public string? Description { get; init; }
    public decimal? Price { get; init; }
    public int? Quantity { get; init; }
}

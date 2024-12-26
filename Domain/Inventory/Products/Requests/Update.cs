using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;

public class Update : IRequest<Product>
{
    public required int Id { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public required int Quantity { get; init; }
}

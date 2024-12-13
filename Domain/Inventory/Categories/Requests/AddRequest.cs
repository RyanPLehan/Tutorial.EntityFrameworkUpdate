using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

public class AddRequest : IRequest<Category>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}

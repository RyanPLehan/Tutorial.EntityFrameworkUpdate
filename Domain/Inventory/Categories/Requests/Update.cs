using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

public class Update : IRequest<Category>
{
    public required int Id { get; init; }
    public required string Description { get; init; }
}

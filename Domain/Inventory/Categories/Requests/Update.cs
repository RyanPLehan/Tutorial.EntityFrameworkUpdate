using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

public class Update : IRequest<Category>
{
    public int Id { get; init; }
    public string Description { get; init; }
}

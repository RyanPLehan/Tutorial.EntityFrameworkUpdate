using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

public class Delete : IRequest
{
    public required Category Category { get; init; }
}

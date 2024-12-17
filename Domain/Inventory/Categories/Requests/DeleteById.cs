using MediatR;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

public class DeleteById : IRequest
{
    public required int Id { get; init; }
}

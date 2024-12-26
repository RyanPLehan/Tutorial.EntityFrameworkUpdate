using MediatR;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;

public class DeleteById : IRequest
{
    public required int Id { get; init; }
}

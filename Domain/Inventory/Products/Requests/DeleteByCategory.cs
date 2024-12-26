using MediatR;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;

public class DeleteByCategory : IRequest
{
    public required int CategoryId { get; init; }
}

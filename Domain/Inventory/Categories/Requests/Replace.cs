using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

public class Replace : IRequest
{
    public required int OldCategoryId { get; init; }
    public required int NewCategoryId { get; init; }
}

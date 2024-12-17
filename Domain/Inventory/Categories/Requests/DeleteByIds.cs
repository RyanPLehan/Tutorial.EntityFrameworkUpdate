using System.Collections.Generic;
using MediatR;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

public class DeleteByIds : IRequest
{
    public required IEnumerable<int> Ids { get; init; }
}

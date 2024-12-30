using System.Collections.Generic;
using MediatR;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Requests;

public class DeleteByIds : IRequest
{
    public required IEnumerable<int> Ids { get; init; }
}

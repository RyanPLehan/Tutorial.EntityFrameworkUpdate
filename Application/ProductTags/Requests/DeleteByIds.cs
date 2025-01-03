using System.Collections.Generic;
using MediatR;

namespace Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;

public class DeleteByIds : IRequest
{
    public required IEnumerable<int> Ids { get; init; }
}
